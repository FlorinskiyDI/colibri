using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.DomainModelLayer.Models.IdentityServer.Pager
{
    public class PageSearchEntry
    {
        public int PageNumber { get; set; }
        public int PageLength { get; set; }

        public List<PageFilterStatementModel> FilterStatements { get; set; }
        public string GlobalSearch { get; set; }

    }
}
