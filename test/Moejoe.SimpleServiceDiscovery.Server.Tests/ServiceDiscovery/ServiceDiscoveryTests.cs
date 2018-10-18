using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Moejoe.SimpleServiceDiscovery.Common.Models;
using Moejoe.SimpleServiceDiscovery.Server.Infrastructure;
using Moejoe.SimpleServiceDiscovery.Server.ServiceDiscovery;
using Moejoe.SimpleServiceDiscovery.Server.Storage.Models;
using Moejoe.SimpleServiceDiscovery.Server.Storage.Stores;
using Moq;
using Xunit;

namespace Moejoe.SimpleServiceDiscovery.Server.Tests.ServiceDiscovery
{
    public class ServiceDiscoveryTests
    {
        public static class Discover
        {
            public class WhenServiceDoesNotExist
            {
                private const string NonExistingServiceName = "IDontExist";

                [Fact]
                public async Task ReturnsEmptyInstances()
                {
                    var store = new Mock<IServiceRegistryStore>();
                    store.Setup(p => p.FindByServiceDefinitionAsync(NonExistingServiceName)).ReturnsAsync(new ServiceInstanceDao[] { });
                    var service = new ServiceDiscoveryService(store.Object, NullLogger<ServiceDiscoveryService>.Instance);
                    var result = await service.DiscoverAsync(NonExistingServiceName);
                    Assert.Empty(result.Instances);
                }
            }

        }
        public class WhenSingleServiceExists
        {
            private static ServiceInstance ExpectedServiceInstance => new ServiceInstance
            {
                Id = "existingService@myAppserver",
                ServiceDefinition = "SingleService",
                BaseUrl = "https://exampleServiceHost/exampleService/api/"
            };

            [Fact]
            public async Task ReturnsServiceBaseUrl()
            {
                var store = new Mock<IServiceRegistryStore>();
                store.Setup(p => p.FindByServiceDefinitionAsync(ExpectedServiceInstance.ServiceDefinition)).ReturnsAsync(new[] { ExpectedServiceInstance.ToDao() });
                var service = new ServiceDiscoveryService(store.Object, NullLogger<ServiceDiscoveryService>.Instance);
                var result = await service.DiscoverAsync(ExpectedServiceInstance.ServiceDefinition);
                Assert.Single(result.Instances);
                Assert.Equal(result.Instances[0].Id, ExpectedServiceInstance.Id);
                Assert.Equal(result.Instances[0].BaseUrl, ExpectedServiceInstance.BaseUrl);
            }
        }
    }
}


