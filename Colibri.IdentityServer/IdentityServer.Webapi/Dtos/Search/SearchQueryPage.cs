using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Dtos.Search
{
    public class SearchQueryPage
    {
        public int PageNumber { get; set; }
        public int PageLength { get; set; }
    }
}
