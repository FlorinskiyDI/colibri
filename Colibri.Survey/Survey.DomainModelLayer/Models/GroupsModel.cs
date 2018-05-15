using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.DomainModelLayer.Models
{
    public class Groups
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
    }
}
