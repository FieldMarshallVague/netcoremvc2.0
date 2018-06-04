using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Logging.Api
{
    public class Startup
    {
        private readonly ILogger logger;
        public Startup(ILogger<Startup> logger)
        {
            this.logger = logger;
            logger.LogInformation(1000, "Logging from constructor!");
            logger.LogError("this is an error");
            logger.LogCritical("here I am!");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            logger.LogInformation(1001, "Logging from Configure!");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages(async context =>
            {
                logger.LogInformation(1002, "Logging from app.Run! Name: {name}, Age: {age}", "toby", 42);
                logger.LogError("this is ALSO an error");
                logger.LogCritical("Rock me like a hurricane!");

                context.HttpContext.Response.ContentType = "text/plain";
                await context.HttpContext.Response.WriteAsync($"Awesome Status Page, status code: {context.HttpContext.Response.StatusCode}");
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
