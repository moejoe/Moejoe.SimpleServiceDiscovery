using System;
using Microsoft.EntityFrameworkCore;
using Moejoe.SimpleServiceDiscovery.EntityFramework.Storage.DbContexts;
using Moejoe.SimpleServiceDiscovery.EntityFramework.Storage.Stores;
using Moejoe.SimpleServiceDiscovery.Server.Configuration;

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
