using Microsoft.Extensions.DependencyInjection;

namespace Moejoe.SimpleServiceDiscovery.WebService.ServiceDiscovery
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDiscoveryService(this IServiceCollection services)
        {
            return services.AddScoped<IServiceDiscoveryService, ServiceDiscoveryService>();
        }
    }
}
