using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace Moejoe.SimpleServiceDiscovery.Example.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var hostBuilder = CreateWebHostBuilder(args);
            await hostBuilder
            .Build()
                .RegisterAndRunAsync("http://localhost:53357", "Example.Api");
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }

    }
}
