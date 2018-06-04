using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mvc.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.LoadFile(@"C:\projects\netcore\AwesomeSauce\C05Mvc.Import\Mvc.Api\bin\Debug\netcoreapp2.0\Mvc.Imports.dll");
            services.AddMvc((options) =>
            {
                var type = typeof(IFilterMetadata);
                var filters = assembly.ExportedTypes.Where(x => type.IsAssignableFrom(x)).Where(t => t.Name.EndsWith("Filter"));

                foreach (var filter in filters)
                {
                    options.Filters.Add(filter);
                }
            })
                .AddApplicationPart(assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
