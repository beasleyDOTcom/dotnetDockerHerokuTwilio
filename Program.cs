using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Data.Entity;

namespace dotnetDockerHerokuTwilio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("heeeeeeee" + Environment.GetEnvironmentVariable("babe"));
            Environment.SetEnvironmentVariable("ASPNETCORE_URLS", "http://*:"+ Environment.GetEnvironmentVariable("PORT"));
            // Environment.SetEnvironmentVariable("ASPNETCORE_HTTPS_PORT", Environment.GetEnvironmentVariable("PORT"));
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
