using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Security.Api
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
            services.AddCors(options =>
            {
                options.AddPolicy("LocalGlobalPolicy", builder => builder.WithOrigins("http://local.dev"));
                options.AddPolicy("LocalPolicy", builder => builder.WithOrigins("http://app1.local.dev"));
                options.AddPolicy("LocalApiPolicy", builder => builder.WithOrigins("http://api.local.dev"));
                options.AddPolicy("LocalApi2Policy", builder => builder.WithOrigins("http://api2.local.dev"));
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var serverSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:ServerSecret"]));
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = serverSecret,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"]
                    };
                });

            services.AddMvc(options =>
            {
                options.SslPort = 44321;
                options.Filters.Add(new RequireHttpsAttribute());
                options.Filters.Add(new CorsAuthorizationFilterFactory("LocalPolicy"));
            });

            services.AddAntiforgery(
                options =>
                {
                    options.Cookie.Name = "_af";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.HeaderName = "X-XSRF-TOKEN";
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("LocalGlobalPolicy");

            // rewrite to SSL
            var options = new RewriteOptions().AddRedirectToHttps((int)HttpStatusCode.MovedPermanently, 44321);
            app.UseRewriter(options);

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
