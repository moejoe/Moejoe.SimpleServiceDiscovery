using System;
using Microsoft.EntityFrameworkCore;
using Moejoe.SimpleServiceDiscovery.EntityFramework.Storage.DbContexts;
using Moejoe.SimpleServiceDiscovery.EntityFramework.Storage.Stores;
using Moejoe.SimpleServiceDiscovery.Storage.Stores;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MoejoeSimpleServiceDiscoveryEntityFrameworkExtensions
    {
        public static IServiceCollection AddServiceRegistryStore(this IServiceCollection services,
            Action<DbContextOptionsBuilder> configureContextOptions)
        {
            services.AddDbContext<ServiceDiscoveryContext>(configureContextOptions);
            return services.AddScoped<IServiceRegistryStore, ServiceRegistryStore>();
        }
    }
}
