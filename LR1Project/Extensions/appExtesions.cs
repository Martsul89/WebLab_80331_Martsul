using LR1Project.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LR1Project.Extensions
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogMiddleware>();
        }
    }
}