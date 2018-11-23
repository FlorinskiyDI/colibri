using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Dtos.Pager
{
    public class SearchEntry
    {
        public List<PageFilterStatement> FilterStatements { get; set; }
        public OrderStatement OrderStatement { get; set; }
        public string GlobalSearch { get; set; }

        public SearchEntry()
        {
            this.FilterStatements = new List<PageFilterStatement>();
        }
    }
}
