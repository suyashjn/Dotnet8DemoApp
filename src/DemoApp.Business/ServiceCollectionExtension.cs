using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DemoApp.Data;
using DemoApp.Business.Helpers;
using DemoApp.Business.Services;
using DemoApp.Business.MapperConfigs;

namespace DemoApp.Business
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            services.AddScoped<ICsvHelperService, CsvHelperService>()
                .AddScoped<IMembersService, MembersService>()
                .AddScoped<IInventoryService, InventoryService>()
                .AddScoped<IBookingsService, BookingsService>();

            services.ConfigureDataServices(configuration);
            return services;
        }
    }
}
