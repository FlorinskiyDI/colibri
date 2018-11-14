using System.Collections.Generic;

namespace Survey.ApplicationLayer.Dtos.Models.IdentityServer.Pager
{
    public class PageSearchEntryDto
    {
        public int PageNumber { get; set; }
        public int PageLength { get; set; }
        public string GlobalSearch { get; set; }
        public List<PageFilterStatementDto> FilterStatements { get; set; }
        public PageOrderStatementDto OrderStatement { get; set; }

        public PageSearchEntryDto()
        {
            this.FilterStatements = new List<PageFilterStatementDto>();
        }
    }
}
