using Core.Database;
using Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Services;

namespace Services
{
    public static class DependencyInjection
    {
        public static void ConfigureCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureEntityFramework(configuration);
            services.ConfigureDtos();
            services.ConfigureServices();
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
        }

    }
}
