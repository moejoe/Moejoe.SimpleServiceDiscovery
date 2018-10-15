using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moejoe.SimpleServiceDiscovery.WebService.Infrastructure;

namespace Moejoe.SimpleServiceDiscovery.WebService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkInMemoryDatabase();
            services.AddDbContext<ServiceDiscoveryContext>(opts =>
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
