using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AwesomeSauce.Api.Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AwesomeSauce.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) {
            return WebHost.CreateDefaultBuilder(args)
                    .UseAwesomeServer(o => o.FolderPath = @"c:\sandbox\in")
                    .UseStartup<Startup>()
                    .Build();
        }

        public static IWebHost BuildWebHost_explicit(string[] args)
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration(config =>
                    config.AddJsonFile("appsettings.json", true)
                )
                .ConfigureLogging(logging =>
                    logging
                        .AddConsole()
                        .AddDebug()
                )
                .UseIISIntegration()
                .UseStartup(startupAssemblyName: "AwesomeSauce.Api")
                .Build();

        }
    }
}
