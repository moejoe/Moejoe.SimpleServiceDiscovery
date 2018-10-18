using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Moejoe.SimpleServiceDiscovery.Common.Models;
using Moejoe.SimpleServiceDiscovery.Server.Infrastructure;
using Moejoe.SimpleServiceDiscovery.Server.ServiceRegistration;
using Moejoe.SimpleServiceDiscovery.Server.Storage.Models;
using Moejoe.SimpleServiceDiscovery.Server.Storage.Stores;
using Moq;
using Xunit;

namespace Moejoe.SimpleServiceDiscovery.Server.Tests.ServiceRegistration
{
    public class ServiceRegistrationTests
    {
        public static class Register
        {
            public class WhenNewServiceDefinition
            {
                private ServiceInstance ServiceInstance => new ServiceInstance
                {
                    Id = "SimpleServiceInstance",
                    BaseUrl = "http://simpleService.instance.com/",
                    ServiceDefinition = "SimpleService"
                };

                [Fact]
                public async Task ReturnsSuccess()
                {
                    var store = new Mock<IServiceRegistryStore>();
                    store.Setup(p => p.GetAsync(ServiceInstance.Id)).ReturnsAsync(default(ServiceInstanceDao));
                    var service = new ServiceRegistrationService(store.Object, NullLogger<ServiceRegistrationService>.Instance);
                    var result = await service.RegisterAsync(ServiceInstance);
                    Assert.False(result.IsError);
                }
            }

            public class WhenServiceInstanceIdExists
            {
                private ServiceInstance ExistingServiceInstance => new ServiceInstance
                {
                    Id = "SimpleServiceInstance",
                    BaseUrl = "http://simpleService.instance.com/",
                    ServiceDefinition = "SimpleService"
                };

                [Fact]
                public async Task DoesNotThrow()
                {
                    var store = new Mock<IServiceRegistryStore>();
                    store.Setup(p => p.GetAsync(ExistingServiceInstance.Id)).ReturnsAsync(ExistingServiceInstance.ToDao());
                    var service = new ServiceRegistrationService(store.Object, NullLogger<ServiceRegistrationService>.Instance);
                    await Assert.ThrowsAsync<ServiceInstanceAlreadyExistsException>(async () => await service.RegisterAsync(ExistingServiceInstance));

                }
            }

        }
        public static class UnRegister
        {
            public class WhenServiceExists
            {
                private ServiceInstance ExistingServiceInstance => new ServiceInstance
                {
                    Id = "SimpleServiceInstance",
                    BaseUrl = "http://simpleService.instance.com/",
                    ServiceDefinition = "SimpleService"
                };

                [Fact]
                public async Task DoesNotThrow()
                {
                    var store = new Mock<IServiceRegistryStore>();
                    store.Setup(p => p.GetAsync(ExistingServiceInstance.Id)).ReturnsAsync(ExistingServiceInstance.ToDao());

                    var service = new ServiceRegistrationService(store.Object, NullLogger<ServiceRegistrationService>.Instance);
                    await service.UnregisterAsync(ExistingServiceInstance.Id);
                }
            }


            public class WhenServiceDoesNotExists
            {
                private ServiceInstance ExistingServiceInstance => new ServiceInstance
                {
                    Id = "SimpleServiceInstance",
                    BaseUrl = "http://simpleService.instance.com/",
                    ServiceDefinition = "SimpleService"
                };

                [Fact]
                public async Task ThrowsException()
                {
                    var store = new Mock<IServiceRegistryStore>();
                    store.Setup(p => p.GetAsync(ExistingServiceInstance.Id)).ReturnsAsync(default(ServiceInstanceDao));
                    var service = new ServiceRegistrationService(store.Object, NullLogger<ServiceRegistrationService>.Instance);
                    await Assert.ThrowsAsync<ServiceInstanceNotFoundException>(async () => await service.UnregisterAsync(ExistingServiceInstance.Id));

                }
            }
        }

    }

}
