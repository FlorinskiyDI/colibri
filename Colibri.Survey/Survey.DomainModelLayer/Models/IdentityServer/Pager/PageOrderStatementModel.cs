using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.DomainModelLayer.Models.IdentityServer.Pager
{
    public class PageOrderStatementModel
    {
        public string ColumName { get; set; }
        public bool Reverse { get; set; }
    }

}
