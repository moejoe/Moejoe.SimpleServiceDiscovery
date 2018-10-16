using System;
using Microsoft.EntityFrameworkCore;
using Moejoe.SimpleServiceDiscovery.Server.Configuration;
using Moejoe.SimpleServiceDiscovery.Server.EntityFramework.Storage.DbContexts;
using Moejoe.SimpleServiceDiscovery.Server.EntityFramework.Storage.Stores;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MoejoeSimpleServiceDiscoveryEntityFrameworkExtensions
    {
        public static IServiceDiscoveryServerBuilder AddServiceRegistryStore(this IServiceDiscoveryServerBuilder builder,
            Action<DbContextOptionsBuilder> configureContextOptions)
        {
            builder.Services.AddDbContext<ServiceDiscoveryContext>(configureContextOptions);
            builder.AddServiceRegistryStore<ServiceRegistryStore>();
            return builder;
        }
    }
}
