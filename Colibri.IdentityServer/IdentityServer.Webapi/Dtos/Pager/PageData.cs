using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Dtos.Pager
{
    public class PageData<T>
    {
        public int PageCount { get; private set; }
        public int TotalItemCount { get; private set; }
        public List<T> ItemList { get; private set; }

        public void PageNumber(int pageCount, int totalItemCount, List<T> itemList)
        {
            this.PageCount = pageCount;
            this.TotalItemCount = totalItemCount;
            this.ItemList = itemList;
        }
    }
}
