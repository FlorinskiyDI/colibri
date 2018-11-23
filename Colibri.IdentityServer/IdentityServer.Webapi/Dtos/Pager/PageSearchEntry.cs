using System.Collections.Generic;
namespace IdentityServer.Webapi.Dtos.Pager
{
    public class PageSearchEntry: SearchEntry
    {
        public int PageNumber { get; set; }
        public int PageLength { get; set; }
    }
}
