using storagecore.EntityFrameworkCore.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.DomainModelLayer.Entities
{
    public partial class Answers : BaseEntity<Guid>
    {
        //public Guid Id { get; set; }
        public double? AnswerNumeric { get; set; }
        public string AnswerText { get; set; }
        public bool AnswerBoolean { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AnswerDateTime { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionOptionId { get; set; }

        [ForeignKey("QuestionOptionId")]
        [InverseProperty("Answers")]
        public QuestionOptions QuestionOption { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Answers")]
        public Users User { get; set; }
    }
}
