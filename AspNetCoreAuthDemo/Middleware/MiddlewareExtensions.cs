using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthDemo.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseEnvironmentDisplay(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EnvironmentDisplay>();
        }
    }
}
