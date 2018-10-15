using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moejoe.SimpleServiceDiscovery.WebService.Infrastructure;
using Moejoe.SimpleServiceDiscovery.WebService.ServiceDiscovery;

namespace Moejoe.SimpleServiceDiscovery.WebService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkInMemoryDatabase();
            services.AddDbContext<ServiceDiscoveryContext>(opts =>
            {
                opts.UseInMemoryDatabase("testDB");
            });
            services.AddDiscoveryService();
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
