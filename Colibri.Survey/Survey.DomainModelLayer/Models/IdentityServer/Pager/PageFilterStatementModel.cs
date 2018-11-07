using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.DomainModelLayer.Models.IdentityServer.Pager
{
    public class PageFilterStatementModel
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        //public OperationBase Operation { get; set; } // temporary
    }
}
