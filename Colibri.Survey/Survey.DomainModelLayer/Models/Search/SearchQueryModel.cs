using System.Collections.Generic;

namespace Survey.DomainModelLayer.Models.Search
{
    public class SearchQueryModel
    {
        public SearchQueryPageModel SearchQueryPage { get; set; }
        public List<FilterStatementModel> FilterStatements { get; set; }
        public OrderStatementModel OrderStatement { get; set; }
        public string GlobalSearch { get; set; }

        public SearchQueryModel()
        {
            this.FilterStatements = new List<FilterStatementModel>();
        }

    }
}
