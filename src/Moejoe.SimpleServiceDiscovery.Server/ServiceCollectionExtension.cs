using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Moejoe.SimpleServiceDiscovery.Server.ServiceDiscovery;
using Moejoe.SimpleServiceDiscovery.Server.ServiceRegistration;

namespace Moejoe.SimpleServiceDiscovery.Server
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services)
        {
            return services.AddScoped<IServiceDiscoveryService, ServiceDiscoveryService>()
                .AddScoped<IServiceRegistrationService, ServiceRegistrationService>();

        }
    }    
}
