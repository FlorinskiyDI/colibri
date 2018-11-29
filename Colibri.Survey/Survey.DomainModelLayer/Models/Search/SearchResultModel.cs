using System.Collections.Generic;

namespace Survey.DomainModelLayer.Models.Search
{
    public class SearchResultModel<T>
    {

        public SearchResultPageModel SearchResultPage { get; set; }
        public List<T> ItemList { get; set; }

        public SearchResultModel()
        {
            this.ItemList = new List<T>();
        }
        
    }
}
