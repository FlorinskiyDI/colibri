using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Webapi.Data3
{
    public partial class Answers
    {
        public Guid Id { get; set; }
        public double? AnswerNumeric { get; set; }
        public string AnswerText { get; set; }
        public bool AnswerBoolean { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AnswerDateTime { get; set; }
        public Guid RespondentId { get; set; }
        public Guid QuestionOptionId { get; set; }

        [ForeignKey("QuestionOptionId")]
        [InverseProperty("Answers")]
        public QuestionOptions QuestionOption { get; set; }
        [ForeignKey("RespondentId")]
        [InverseProperty("Answers")]
        public Respondents Respondent { get; set; }
    }
}
