using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Builder;
using MoeJoe.SimpleServiceDiscovery.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Threading;

namespace Moejoe.SimpleServiceDiscovery.Client.Tests
{
    public class DiscoverClientTests
    {

        private static string NotFoundResponseBody = Newtonsoft.Json.JsonConvert.SerializeObject(new Error
        {
            Type = DiscoveryApi.ErrorTypes.ServiceNotFound
        });


        class StaticResponseTestServerStartUp
        {
            private readonly int _statusCode;
            private readonly string _contentType;
            private readonly string _responseBody;

            protected StaticResponseTestServerStartUp(int statusCode, string contentType, string responseBody = null)
            {
                _statusCode = statusCode;
                _contentType = contentType;
                _responseBody = responseBody;
            }
            public void Configure(IApplicationBuilder app)
            {

                app.Run(async ctx =>
                {
                    ctx.Response.StatusCode = _statusCode;
                    ctx.Response.ContentType = _contentType;
                    if (!string.IsNullOrWhiteSpace(_responseBody))
                    {
                        using (var writer = new StreamWriter(ctx.Response.Body))
                        {
                            await writer.WriteAsync(_responseBody);
                            await ctx.Response.Body.FlushAsync();
                        }
                    }
                });
            }
        }

        private static TestServer CreateTestServer<TMockServerStartUp>() where TMockServerStartUp : class
        {

            return new TestServer(new WebHostBuilder()
                .UseStartup<TMockServerStartUp>());

        }

        public static class Discover
        {
            public class WhenServiceDoesNotExist
            {

                class NotFoundMockServerStartUp : StaticResponseTestServerStartUp
                {
                    public NotFoundMockServerStartUp() : base(404, "application/json", NotFoundResponseBody)
                    {

                    }
                }

                private const string NonExistingServiceName = "IDontExist";

                [Fact]
                public void ReturnsError()
                {
                    using (var server = CreateTestServer<NotFoundMockServerStartUp>())
                    {
                        var service = new DiscoveryClient(server.CreateClient());

                        var result = service.Discover(NonExistingServiceName);

                        Assert.True(result.IsError);
                        Assert.Null(result.BaseUrl);
                    }
                }

            }

            public class WhenSingleServiceExists
            {
                private const string ExistingServiceName = "SingleService";
                private const string ExistingServiceBaseUrl = "http://myappserver:8190/existingService/";

                private static string ResponseBody = Newtonsoft.Json.JsonConvert.SerializeObject(new[]{ new ServiceInstance
                {
                    Id = "1234",
                    ServiceDefinition = ExistingServiceName,
                    BaseUrl = ExistingServiceBaseUrl
                } });
                class SingleServiceResponseMockServer : StaticResponseTestServerStartUp
                {
                    public SingleServiceResponseMockServer() : base(200, "application/json", ResponseBody)
                    {

                    }
                }

                [Theory]
                [InlineData(ExistingServiceName, ExistingServiceBaseUrl)]
                public void ReturnsServiceBaseUrl(string serviceName, string expectedBaseUrl)
                {
                    var server = CreateTestServer<SingleServiceResponseMockServer>();
                    var service = new DiscoveryClient(server.CreateClient());
                    var result = service.Discover(serviceName);
                    Assert.False(result.IsError);
                    Assert.Equal(expectedBaseUrl, result.BaseUrl.ToString());
                }
            }
            public class WhenUnexpectedResponse
            {
                private const string DummyServiceName = "DummyService";
                private static string ResponseBody = Newtonsoft.Json.JsonConvert.SerializeObject(new Error
                {
                    Type = "http://unexpected.errors/",

                });
                class UnexpectedErrorResponseMockServer : StaticResponseTestServerStartUp
                {
                    public UnexpectedErrorResponseMockServer() : base(422, "application/json", ResponseBody)
                    {

                    }
                }
                [Fact]
                public async Task ThrowsException()
                {
                    using (var server = CreateTestServer<UnexpectedErrorResponseMockServer>())
                    {
                        var service = new DiscoveryClient(server.CreateClient());

                        await Assert.ThrowsAsync<DiscoveryException>(async () => await service.DiscoverAsync(DummyServiceName, CancellationToken.None));
                    }
                }

            }
        }
    }
}
