using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.DomainModelLayer.Models.IdentityServer.Pager
{
    public class PageSearchEntryModel
    {
        public int PageNumber { get; set; }
        public int PageLength { get; set; }
        public string GlobalSearch { get; set; }
        public List<PageFilterStatementModel> FilterStatements { get; set; }
        public PageOrderStatementModel OrderStatement { get; set; }

        public PageSearchEntryModel()
        {
            this.FilterStatements = new List<PageFilterStatementModel>();
        }

    }
}
