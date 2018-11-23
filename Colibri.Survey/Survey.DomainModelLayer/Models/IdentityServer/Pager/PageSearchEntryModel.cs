using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.DomainModelLayer.Models.IdentityServer.Pager
{
    public class PageSearchEntryModel: SearchEntryModel
    {
        public int PageNumber { get; set; }
        public int PageLength { get; set; }
    }
}
