using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using BookLibrary.Api.Utils;
using BookLibrary.Core.Interfaces;
using BookLibrary.Core.Services;
using BookLibrary.Infrastructure;
using BookLibrary.Infrastructure.Repositories;

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

builder.Services.AddControllers(options => options.Filters.Add(typeof(ErrorResultFilter)));

builder.Services.AddDbContext<BookLibraryDbContext>(options => options.UseInMemoryDatabase("BookLibrary"));
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(BaseService<>));
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        using (var context = scope.ServiceProvider.GetRequiredService<BookLibraryDbContext>())
        {
            context.Database.EnsureCreated();
        }
    }
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();