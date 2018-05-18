using storagecore.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.DomainModelLayer.Entities
{
    public partial class Pages : BaseEntity<Guid>
    {
        public Pages()
        {
            Questions = new HashSet<Questions>();
        }

        //public Guid Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderNo { get; set; }
        public Guid SurveyId { get; set; }

        [ForeignKey("SurveyId")]
        [InverseProperty("Pages")]
        public SurveySections Survey { get; set; }
        [InverseProperty("Page")]
        public ICollection<Questions> Questions { get; set; }
    }
}
