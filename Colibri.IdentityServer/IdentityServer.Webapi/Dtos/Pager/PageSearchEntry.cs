using System.Collections.Generic;
namespace IdentityServer.Webapi.Dtos.Pager
{
    public class PageSearchEntry
    {
        public int PageNumber { get; set; }
        public int PageLength { get; set; }

        public List<PageFilterStatement> FilterStatements { get; set; }
        public string GlobalSearch { get; set; }

    }
}
