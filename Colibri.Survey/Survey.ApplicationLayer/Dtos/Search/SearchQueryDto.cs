using System.Collections.Generic;

namespace Survey.ApplicationLayer.Dtos.Search
{
    public class SearchQueryDto
    {
        public SearchQueryPageDto SearchQueryPage { get; set; }
        public List<FilterStatementDto> FilterStatements { get; set; }
        public OrderStatementDto OrderStatement { get; set; }
        public string GlobalSearch { get; set; }

        public SearchQueryDto()
        {
            this.FilterStatements = new List<FilterStatementDto>();
        }

    }
}
