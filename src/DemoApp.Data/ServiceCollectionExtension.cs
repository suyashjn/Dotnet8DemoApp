using DemoApp.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DemoApp.Data
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("Default")));

            services.AddScoped<IMembersRepository, MembersRepository>()
                .AddScoped<IInventoryRepository, InventoryRepository>()
                .AddScoped<IBookingsRepository, BookingsRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();




            return services;
        }
    }
}
