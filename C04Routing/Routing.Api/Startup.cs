using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Routing.Api
{
    public class Startup
    {
        /*  routing with a hosted service for gettting a dog pic url every 10 seconds.
         * 
         * */
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddSingleton<IHostedService, AwesomeHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouter(builder =>
            {
                builder.MapRoute(string.Empty, context =>
                {
                    var routeValues = new RouteValueDictionary
                    {
                        { "number", 456 }
                    };

                    var vpc = new VirtualPathContext(context, null, routeValues, "bar/{number:int}");
                    var route = builder.Routes.Single(r => r.ToString().Equals(vpc.RouteName));
                    var barUrl = route.GetVirtualPath(vpc).VirtualPath;

                    return context.Response.WriteAsync($"URL: {barUrl}");
                });

                builder.MapGet("foo/{name}/{surname?}", (request, response, routeData) =>
                {
                    return response.WriteAsync($"Welcome to the Foo, {routeData.Values["name"]} {routeData.Values["surName"]} ");
                });

                builder.MapGet("bar/{number:int}", (request, response, routeData) =>
                {
                    return response.WriteAsync($"Welcome to the Bar, {routeData.Values["number"]} ");
                });
            });
        }
    }
}
