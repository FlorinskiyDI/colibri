using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Webapi.Data3
{
    [Table("Question_Options")]
    public partial class QuestionOptions
    {
        public QuestionOptions()
        {
            Answers = new HashSet<Answers>();
        }

        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid OptionChoiseId { get; set; }

        [ForeignKey("OptionChoiseId")]
        [InverseProperty("QuestionOptions")]
        public OptionChoises OptionChoise { get; set; }
        [ForeignKey("QuestionId")]
        [InverseProperty("QuestionOptions")]
        public Questions Question { get; set; }
        [InverseProperty("QuestionOption")]
        public ICollection<Answers> Answers { get; set; }
    }
}
