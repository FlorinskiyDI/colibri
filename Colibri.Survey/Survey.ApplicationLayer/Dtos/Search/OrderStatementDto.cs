using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Dtos.Search
{
    public class OrderStatementDto
    {
        public string ColumName { get; set; }
        public bool Reverse { get; set; }
    }
}
