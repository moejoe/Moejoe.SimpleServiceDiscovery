﻿using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Moejoe.SimpleServiceDiscovery.WebService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //logger.Debug("Starting app");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //logger.Error(ex, "Program stopped because of exception");
                throw;
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            //.ConfigureLogging( logging => {


            //})
            ;
    }
}
