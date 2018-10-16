using Xunit;
using Moejoe.SimpleServiceDiscovery.Common;
using Moejoe.SimpleServiceDiscovery.Common.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Threading;

namespace Moejoe.SimpleServiceDiscovery.Client.Tests
{
    public static class RegistrationClientTests
    {
        public static class Register
        {
            public class WhenValidServiceInstance
            {
                private static ServiceInstance ServiceInstance => new ServiceInstance
                {
                    Id = "1234",
                    ServiceDefinition = "TestServiceDefintion",
                    BaseUrl = "http://myappserver:8190/testServiceDefinition/"
                };

                class SuccessMockServerStartUp : TestHelper.StaticResponseTestServerStartUp
                {
                    public SuccessMockServerStartUp() : base(201, null, null)
                    {
                    }
                }

                [Fact]
                public async Task ReturnsSuccess()
                {
                    var testServer = new TestServer(new WebHostBuilder()
                        .UseStartup<SuccessMockServerStartUp>());

                    var client = new RegistrationClient(testServer.CreateClient());
                    var response = await client.RegisterAsync(ServiceInstance, CancellationToken.None);
                    Assert.False(response.IsError);
                }
            }
        }
        public class WhenInvalidServiceInstance
        {
            private static string BadRequestErrorResponse => Newtonsoft.Json.JsonConvert.SerializeObject(new Error
            {
                Type = DiscoveryApi.ErrorTypes.InvalidArgumentType,
                Title = "Some Title",
                Detail = "Some Error Detail"
            });
            private static ServiceInstance ServiceInstance => new ServiceInstance
            {
                Id = "1234",
                ServiceDefinition = "TestServiceDefintion",
                BaseUrl = "http://myappserver:8190/testServiceDefinition/"
            };
            class BadRequestMockServerStartUp : TestHelper.StaticResponseTestServerStartUp
            {
                public BadRequestMockServerStartUp() : base(400, "application/json", BadRequestErrorResponse)
                {

                }
            }

            [Fact]
            public async Task ThrowsRegistrationException()
            {
                var testServer = new TestServer(new WebHostBuilder()
                       .UseStartup<BadRequestMockServerStartUp>());

                var client = new RegistrationClient(testServer.CreateClient());
                await Assert.ThrowsAsync<DiscoveryException>(async () => await client.RegisterAsync(ServiceInstance, CancellationToken.None));
            }
        }

    }
}
