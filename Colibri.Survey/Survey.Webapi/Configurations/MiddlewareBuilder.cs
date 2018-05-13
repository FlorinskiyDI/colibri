using Microsoft.AspNetCore.Builder;
using Survey.Webapi.Configurations.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Webapi.Configurations
{
    public static class MiddlewareBuilder
    {
        public static IApplicationBuilder UseProfileMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ProfileMiddleWare>();
        }
        public static IApplicationBuilder UseContextMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ContextMiddleWare>();
        }        
    }
}
