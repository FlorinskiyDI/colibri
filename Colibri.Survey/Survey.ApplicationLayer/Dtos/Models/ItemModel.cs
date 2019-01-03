using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models
{
    public class ItemModel
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public bool IsAdditionalChoise { get; set; }
    }
}
