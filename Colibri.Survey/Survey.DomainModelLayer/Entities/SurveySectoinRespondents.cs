using storagecore.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.DomainModelLayer.Entities
{
    [Table("SurveySectoin_Respondents")]
    public partial class SurveySectoinRespondents : BaseEntity<Guid>
    {
        //public Guid Id { get; set; }
        public Guid RespondentId { get; set; }
        public Guid SurveySectionId { get; set; }

        [ForeignKey("RespondentId")]
        [InverseProperty("SurveySectoinRespondents")]
        public Respondents Respondent { get; set; }
        [ForeignKey("SurveySectionId")]
        [InverseProperty("SurveySectoinRespondents")]
        public SurveySections SurveySection { get; set; }
    }
}
