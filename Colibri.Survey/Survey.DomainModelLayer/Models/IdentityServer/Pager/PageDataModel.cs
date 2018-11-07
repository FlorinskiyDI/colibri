using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.DomainModelLayer.Models.IdentityServer.Pager
{
    public class PageDataModel<T>
    {
        public int PageCount { get; set; }
        public int TotalItemCount { get; set; }
        public List<T> ItemList { get;  set; } 
    }
}
