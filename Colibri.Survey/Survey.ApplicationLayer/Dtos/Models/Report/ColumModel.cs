using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models.Report
{
    public class ColumModel
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int OrderNo { get; set; }
        public List<string> Children { get; set; }
        public Guid? ParentId { get; set; }
        public ColumModel()
        {
            this.Children = new List<string>();
        }
    }
}
