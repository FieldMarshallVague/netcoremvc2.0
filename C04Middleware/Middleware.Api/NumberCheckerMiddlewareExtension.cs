using Microsoft.AspNetCore.Builder;
using Middleware.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public static class NumberCheckerMiddlewareExtension
    {
        public static IApplicationBuilder UseNumberChecker(this IApplicationBuilder app)
        {
            return app.UseMiddleware<NumberCheckerMiddleware>();
        }
    }
}
