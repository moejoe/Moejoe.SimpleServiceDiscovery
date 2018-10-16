using System;
using Microsoft.Extensions.DependencyInjection;

namespace Moejoe.SimpleServiceDiscovery.Server.Configuration
{
    public class ServiceDiscoveryServerBuilder : IServiceDiscoveryServerBuilder
    {
        public ServiceDiscoveryServerBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));            
        }
        public IServiceCollection Services { get; }
    }
}

namespace Microsoft.Extensions.DependencyInjection
{
}