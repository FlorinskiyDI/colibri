using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.DomainModelLayer.Models.Search
{
    public class OrderStatementModel
    {
        public string ColumName { get; set; }
        public bool Reverse { get; set; }
    }
}
