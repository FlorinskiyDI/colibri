using System.Collections.Generic;

namespace Survey.ApplicationLayer.Dtos.Models.IdentityServer.Pager
{
    public class PageSearchEntryDto
    {
        public int PageNumber { get; set; }
        public int PageLength { get; set; }

        public List<PageFilterStatementDto> FilterStatements { get; set; }
        public string GlobalSearch { get; set; }
    }
}
