using BookLibrary.Core.Interfaces;
using BookLibrary.Infrastructure;
using BookLibrary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterInfra
    {
        public static IServiceCollection RegisterInfraDependencies(this IServiceCollection services)
        {
            services.AddDbContext<BookLibraryDbContext>(options => options.UseInMemoryDatabase("BookLibrary"));
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
