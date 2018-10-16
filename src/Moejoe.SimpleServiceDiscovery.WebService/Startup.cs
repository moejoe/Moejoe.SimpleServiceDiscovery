using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moejoe.SimpleServiceDiscovery.EntityFramework.Storage.DbContexts;
using Moejoe.SimpleServiceDiscovery.Server;

namespace Moejoe.SimpleServiceDiscovery.WebService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkInMemoryDatabase();
            services.AddServiceRegistryStore(opts =>
            {
                opts.UseInMemoryDatabase("testDB");
            });
            services.AddServiceDiscovery();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
