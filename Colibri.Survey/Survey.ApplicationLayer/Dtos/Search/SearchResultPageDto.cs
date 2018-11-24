using System;

namespace Survey.ApplicationLayer.Dtos.Search
{
    public class SearchResultPageDto
    {
        public long TotalItemCount { get; set; }
        public int TotalPageCount => Convert.ToInt32(Math.Ceiling((decimal)TotalItemCount / PageLength));

        public int PageNumber { get; set; }
        public int PageLength { get; set; }
    }
}
