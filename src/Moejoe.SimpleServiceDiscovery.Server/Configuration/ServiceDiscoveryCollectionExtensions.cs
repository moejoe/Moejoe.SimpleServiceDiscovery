using Moejoe.SimpleServiceDiscovery.Server.Configuration;
using Moejoe.SimpleServiceDiscovery.Server.ServiceDiscovery;
using Moejoe.SimpleServiceDiscovery.Server.ServiceRegistration;
using Moejoe.SimpleServiceDiscovery.Server.Stores.InMemory;
using Moejoe.SimpleServiceDiscovery.Storage.Stores;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceDiscoveryCollectionExtensions
    {
        public static IServiceDiscoveryServerBuilder AddServiceDiscoveryServerBuilder(this IServiceCollection services)
        {
            return new ServiceDiscoveryServerBuilder(services);
        }


        public static IServiceDiscoveryServerBuilder AddServiceDiscoveryServer(this IServiceCollection services)
        {
            var builder = services.AddServiceDiscoveryServerBuilder();

            builder
                .AddCoreServices();

            builder.AddInMemoryServiceRegistryStore();
            return builder;
        }
    }

    public static class ServiceDiscoveryServerBuilderExtensionsCore
    {
        public static IServiceDiscoveryServerBuilder AddCoreServices(this IServiceDiscoveryServerBuilder builder)
        {
            builder.Services
                .AddTransient<IServiceDiscoveryService, ServiceDiscoveryService>()
                .AddTransient<IServiceRegistrationService, ServiceRegistrationService>();

            return builder;
        }

        public static IServiceDiscoveryServerBuilder AddServiceRegistryStore<TStore>(
            this IServiceDiscoveryServerBuilder builder) where TStore : class, IServiceRegistryStore
        {
            builder.Services.AddTransient<IServiceRegistryStore, TStore>();
            return builder;
        }
    }

    public static class ServiceDiscoveryServerBuilderExtensionsInMemory
    {
        public static IServiceDiscoveryServerBuilder AddInMemoryServiceRegistryStore(
            this IServiceDiscoveryServerBuilder builder)
        {
            builder.AddServiceRegistryStore<InMemoryServiceRegistryStore>();

            return builder;
        }
    }

}