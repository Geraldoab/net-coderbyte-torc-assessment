using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using BookLibrary.Api.Utils;
using BookLibrary.Core.Interfaces;
using BookLibrary.Core.Services;
using BookLibrary.Infrastructure;
using BookLibrary.Infrastructure.Repositories;
using BookLibrary.Application.Interfaces;
using BookLibrary.Application;
using BookLibrary.Application.Interfaces.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Book Library API",
        Version = "v1",
    });
});

const string ROYAL_LIBRARY_WEB_APP_POLICY_NAME = "RoyalLibraryWebAppAccess";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: ROYAL_LIBRARY_WEB_APP_POLICY_NAME,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Front-End URL
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddControllers(options => options.Filters.Add(typeof(ErrorResultFilter)));

builder.Services.RegisterInfraDependencies();
builder.Services.RegisterApplicationDependencies();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    using (var context = scope.ServiceProvider.GetRequiredService<BookLibraryDbContext>())
    {
        context.Database.EnsureCreated();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors(ROYAL_LIBRARY_WEB_APP_POLICY_NAME);
app.MapControllers();
app.Run();