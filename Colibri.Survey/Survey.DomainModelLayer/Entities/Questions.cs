using storagecore.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.DomainModelLayer.Entities
{
    public partial class Questions : BaseEntity<Guid>
    {
        public Questions()
        {
            InverseParent = new HashSet<Questions>();
            QuestionOptions = new HashSet<QuestionOptions>();
        }

        //public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public int OrderNo { get; set; }
        public bool AdditionalAnswer { get; set; }
        public bool AllowMultipleOptionAnswers { get; set; }
        public Guid InputTypesId { get; set; }
        public Guid PageId { get; set; }
        public Guid OptionGroupId { get; set; }

        [ForeignKey("InputTypesId")]
        [InverseProperty("Questions")]
        public InputTypes InputTypes { get; set; }
        [ForeignKey("OptionGroupId")]
        [InverseProperty("Questions")]
        public OptionGroups OptionGroup { get; set; }
        [ForeignKey("PageId")]
        [InverseProperty("Questions")]
        public Pages Page { get; set; }
        [ForeignKey("ParentId")]
        [InverseProperty("InverseParent")]
        public Questions Parent { get; set; }
        [InverseProperty("Parent")]
        public ICollection<Questions> InverseParent { get; set; }
        [InverseProperty("Question")]
        public ICollection<QuestionOptions> QuestionOptions { get; set; }
    }
}
