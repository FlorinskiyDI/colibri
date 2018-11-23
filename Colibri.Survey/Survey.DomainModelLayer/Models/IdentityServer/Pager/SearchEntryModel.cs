using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.DomainModelLayer.Models.IdentityServer.Pager
{
    public class SearchEntryModel
    {
        public List<PageFilterStatementModel> FilterStatements { get; set; }
        public PageOrderStatementModel OrderStatement { get; set; }
        public string GlobalSearch { get; set; }

        public SearchEntryModel()
        {
            this.FilterStatements = new List<PageFilterStatementModel>();
        }
    }
}
