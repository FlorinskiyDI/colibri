using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Infrastructure.Razor
{
    public interface IViewRenderResolver
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
