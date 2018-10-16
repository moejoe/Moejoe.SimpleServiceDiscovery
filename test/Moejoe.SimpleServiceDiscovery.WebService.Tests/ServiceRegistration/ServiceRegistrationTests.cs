using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moejoe.SimpleServiceDiscovery.Common.Models;
using Moejoe.SimpleServiceDiscovery.Server.Infrastructure;
using Moejoe.SimpleServiceDiscovery.Server.ServiceRegistration;
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
                    var opts = new DbContextOptionsBuilder<ServiceDiscoveryContext>()
                        .UseInMemoryDatabase("WhenNewServiceDefinition")
                        .Options;
                    using (var context = new ServiceDiscoveryContext(opts))
                    {
                        var service = new ServiceRegistrationService(context);
                        var result = await service.RegisterAsync(ServiceInstance);
                        Assert.False(result.IsError);
                    }
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
                    var opts = new DbContextOptionsBuilder<ServiceDiscoveryContext>()
                        .UseInMemoryDatabase("WhenServiceInstanceIdExists")
                        .Options;

                    using (var context = new ServiceDiscoveryContext(opts))
                    {
                        context.ServiceInstances.Add(ExistingServiceInstance.ToDao());
                        await context.SaveChangesAsync();
                        var service = new ServiceRegistrationService(context);
                        await Assert.ThrowsAsync<ServiceInstanceAlreadyExistsException>(async () => await service.RegisterAsync(ExistingServiceInstance));
                    }
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
                    var opts = new DbContextOptionsBuilder<ServiceDiscoveryContext>()
                        .UseInMemoryDatabase("WhenServiceExists")
                        .Options;

                    using (var context = new ServiceDiscoveryContext(opts))
                    {
                        context.ServiceInstances.Add(ExistingServiceInstance.ToDao());
                        await context.SaveChangesAsync();
                        var service = new ServiceRegistrationService(context);
                        await service.UnregisterAsync(ExistingServiceInstance.Id);
                    }
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
                    var opts = new DbContextOptionsBuilder<ServiceDiscoveryContext>()
                        .UseInMemoryDatabase("test")
                        .Options;

                    using (var context = new ServiceDiscoveryContext(opts))
                    {
                        context.ServiceInstances.Add(ExistingServiceInstance.ToDao());
                        var service = new ServiceRegistrationService(context);
                        await Assert.ThrowsAsync<ServiceInstanceNotFoundException>(async () => await service.UnregisterAsync(ExistingServiceInstance.Id));
                    }
                }
            }
        }

    }

}
