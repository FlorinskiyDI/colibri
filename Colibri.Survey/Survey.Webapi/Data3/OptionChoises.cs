using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Webapi.Data3
{
    public partial class OptionChoises
    {
        public OptionChoises()
        {
            QuestionOptions = new HashSet<QuestionOptions>();
        }

        public Guid Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public Guid OptionGroupId { get; set; }
        public int OrderNo { get; set; }

        [ForeignKey("OptionGroupId")]
        [InverseProperty("OptionChoises")]
        public OptionGroups OptionGroup { get; set; }
        [InverseProperty("OptionChoise")]
        public ICollection<QuestionOptions> QuestionOptions { get; set; }
    }
}
