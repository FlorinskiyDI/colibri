using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models.IdentityServer.Pager
{
    public class SearchEntryDto
    {
        public List<PageFilterStatementDto> FilterStatements { get; set; }
        public PageOrderStatementDto OrderStatement { get; set; }
        public string GlobalSearch { get; set; }

        public SearchEntryDto()
        {
            this.FilterStatements = new List<PageFilterStatementDto>();
        }
    }
}
