using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Entities
{
    public class SurveySectionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsShowDescription { get; set; }
        public bool IsShowProcessCompletedText { get; set; }
        public string ProcessCompletedText { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UserId { get; set; }
        public bool IsOpenAccess { get; set; }
        //public ICollection<PagesDto> Pages { get; set; }
    }
}
