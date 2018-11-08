using dataaccesscore.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.DomainModelLayer.Entities
{
    public partial class Respondents : BaseEntity<Guid>
    {
        public Respondents()
        {
            Answers = new HashSet<Answers>();
            SurveySectoinRespondents = new HashSet<SurveySectoinRespondents>();
        }

        //public Guid Id { get; set; }
        public Guid? UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Respondents")]
        public Users User { get; set; }
        [InverseProperty("Respondent")]
        public ICollection<Answers> Answers { get; set; }
        [InverseProperty("Respondent")]
        public ICollection<SurveySectoinRespondents> SurveySectoinRespondents { get; set; }
    }
}
