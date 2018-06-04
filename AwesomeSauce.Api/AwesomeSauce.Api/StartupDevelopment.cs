using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeSauce.Api
{
    public class StartupDevelopment
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IComponent, ComponentA>();
            services.AddSingleton<IComponent, ComponentB>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IComponent component)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/foo",
                config =>
                    config.Use(async (context, next) =>
                        await context.Response.WriteAsync($"Welcome to Foo.")
                    )
                );

            app.MapWhen(
                context =>
                    context.Request.Method == "POST" &&
                    context.Request.Path == "/bar",
                config =>
                    config.Use(async (context, next) =>
                    {
                        await context.Response.WriteAsync("Welcome to the POST /bar");
                    })
                );


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Hello world!  I'm running DEBUG mode and the component is {component.Name}");
            });
        }
    }
}
