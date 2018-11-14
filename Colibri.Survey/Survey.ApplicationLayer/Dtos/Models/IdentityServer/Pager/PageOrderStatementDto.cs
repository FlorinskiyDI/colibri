using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models.IdentityServer.Pager
{
    public class PageOrderStatementDto
    {
        public string ColumName { get; set; }
        public bool Reverse { get; set; }
    }
}
