using System.Collections.Generic;

namespace IdentityServer.Webapi.Dtos.Search
{
    public class SearchQuery
    {
        public SearchQueryPage SearchQueryPage { get; set; }
        public List<FilterStatement> FilterStatements { get; set; }
        public OrderStatement OrderStatement { get; set; }
        public string GlobalSearch { get; set; }

        public SearchQuery()
        {
            this.FilterStatements = new List<FilterStatement>();
        }

    }
}
