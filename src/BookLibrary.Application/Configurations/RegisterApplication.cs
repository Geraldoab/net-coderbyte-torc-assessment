using BookLibrary.Application;
using BookLibrary.Application.Interfaces;
using BookLibrary.Application.Interfaces.Events;
using BookLibrary.Core.Interfaces;
using BookLibrary.Core.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterApplication
    {
        public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IBookNotification, BookNotification>();
            services.AddScoped(typeof(IService<>), typeof(BaseService<>));

            return services;
        }
    }
}
