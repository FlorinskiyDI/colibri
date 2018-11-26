using System;

namespace Survey.DomainModelLayer.Models.Search
{
    public class FilterStatementModel
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        //public OperationBase Operation { get; set; } // temporary
    }
}
