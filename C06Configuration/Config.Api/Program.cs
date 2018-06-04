using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Config.Api
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            //var configBuilder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile(config =>
            //    {
            //        config.Path = "awesomeConfig.json";
            //        config.ReloadOnChange = true;
            //    });
            //Configuration = configBuilder.Build();

            //foreach (var item in Configuration.AsEnumerable())
            //{
            //    Console.WriteLine($"Key: {item.Key}, Value: {item.Key}");
            //}

            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.UseConfiguration(Configuration)
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
        }
    }
}
