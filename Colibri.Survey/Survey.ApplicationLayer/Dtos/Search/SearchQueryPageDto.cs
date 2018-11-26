using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Dtos.Search
{
    public class SearchQueryPageDto
    {
        public int PageNumber { get; set; }
        public int PageLength { get; set; }
    }
}
