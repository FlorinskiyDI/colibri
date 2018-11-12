using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models.IdentityServer.Pager
{
    public class PageFilterStatementDto
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        //public OperationBase Operation { get; set; } // temporary
    }
}
