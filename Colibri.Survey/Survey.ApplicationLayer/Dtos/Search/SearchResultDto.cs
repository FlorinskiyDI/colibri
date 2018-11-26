using System.Collections.Generic;

namespace Survey.ApplicationLayer.Dtos.Search
{
    public class SearchResultDto<T>
    {

        public SearchResultPageDto SearchResultPage { get; set; }
        public List<T> ItemList { get; set; }

        public SearchResultDto()
        {
            this.ItemList = new List<T>();
        }
        
    }
}
