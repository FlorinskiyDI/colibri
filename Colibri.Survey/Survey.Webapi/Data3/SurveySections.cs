using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Webapi.Data3
{
    public partial class SurveySections
    {
        public SurveySections()
        {
            Pages = new HashSet<Pages>();
            SurveySectoinRespondents = new HashSet<SurveySectoinRespondents>();
        }

        public Guid Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("SurveySections")]
        public Users User { get; set; }
        [InverseProperty("Survey")]
        public ICollection<Pages> Pages { get; set; }
        [InverseProperty("SurveySection")]
        public ICollection<SurveySectoinRespondents> SurveySectoinRespondents { get; set; }
    }
}
