using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Lib
{
    public class SkipAppMiddleware
    {
        public RequestDelegate next;

        public SkipAppMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"Skip the line!");
        }
    }
}
