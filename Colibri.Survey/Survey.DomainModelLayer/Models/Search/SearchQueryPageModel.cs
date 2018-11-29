using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.DomainModelLayer.Models.Search
{
    public class SearchQueryPageModel
    {
        public int PageNumber { get; set; }
        public int PageLength { get; set; }
    }
}
