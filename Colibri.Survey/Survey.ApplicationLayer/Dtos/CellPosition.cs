using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos
{
    public class CellPosition
    {
        public int FromRow { get; set; }
        public int FromColumn { get; set; }
        public int ToRow { get; set; }
        public int ToColumn { get; set; }
    }
}
