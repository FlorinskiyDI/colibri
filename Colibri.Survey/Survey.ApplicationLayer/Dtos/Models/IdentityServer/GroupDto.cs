using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models
{
    public class GroupDto
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public int CountChildren { get; set; }
        public string GroupID { get; set; }
        public string Description { get; set; }
    }
}
