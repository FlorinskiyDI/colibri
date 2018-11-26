using System;

namespace Survey.ApplicationLayer.Dtos.Search
{
    public class FilterStatementDto
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        //public OperationBase Operation { get; set; } // temporary
    }
}
