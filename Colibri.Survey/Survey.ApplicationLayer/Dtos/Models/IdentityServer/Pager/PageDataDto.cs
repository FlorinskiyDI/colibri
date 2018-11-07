using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models.IdentityServer.Pager
{
    public class PageDataDto<T>
    {
        public int PageCount { get; set; }
        public int TotalItemCount { get; set; }
        public List<T> ItemList { get; set; }
    }
}
