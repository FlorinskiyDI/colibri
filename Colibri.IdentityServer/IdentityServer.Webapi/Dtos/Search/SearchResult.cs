using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Dtos.Search
{
    public class SearchResult<T>
    {

        public SearchResultPage SearchResultPage { get; set; }
        public List<T> ItemList { get; set; }

        public SearchResult()
        {
            this.ItemList = new List<T>();
        }
        
    }
}
