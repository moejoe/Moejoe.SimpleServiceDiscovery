using Moejoe.SimpleServiceDiscovery.WebService.ServiceDiscovery;
using Microsoft.EntityFrameworkCore;
using Moejoe.SimpleServiceDiscovery.WebService.Infrastructure;
using Moejoe.SimpleServiceDiscovery.Common.Models;
using Xunit;
using System.Threading.Tasks;


namespace Moejoe.SimpleServiceDiscovery.WebService.Tests
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
                    var opts = new DbContextOptionsBuilder<ServiceDiscoveryContext>()
                        .UseInMemoryDatabase("test")
                        .Options;
                    using (var context = new ServiceDiscoveryContext(opts))
                    {
                        var service = new ServiceDiscoveryService(context);
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
                    var opts = new DbContextOptionsBuilder<ServiceDiscoveryContext>()
                        .UseInMemoryDatabase("test")
                        .Options;
                    using (var context = new ServiceDiscoveryContext(opts))
                    {
                        context.ServiceInstances.Add(ExpectedServiceInstance.ToDao());
                        await context.SaveChangesAsync();

                        var svc = new ServiceDiscoveryService(context);
                        var result = await svc.DiscoverAsync(ExpectedServiceInstance.ServiceDefinition);
                        Assert.Single(result.Instances);
                        Assert.Equal(result.Instances[0].Id, ExpectedServiceInstance.Id);
                        Assert.Equal(result.Instances[0].BaseUrl, ExpectedServiceInstance.BaseUrl);
                    }
                }
            }
        }
    }

}
