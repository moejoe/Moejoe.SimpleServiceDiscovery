using Microsoft.Extensions.DependencyInjection;
using Moejoe.SimpleServiceDiscovery.WebService.ServiceDiscovery;
using Moejoe.SimpleServiceDiscovery.WebService.ServiceRegistration;

namespace Moejoe.SimpleServiceDiscovery.WebService
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services)
        {
            return services.AddScoped<IServiceDiscoveryService, ServiceDiscoveryService>()
                .AddScoped<IServiceRegistrationService, ServiceRegistrationService>();

        }
    }
}
