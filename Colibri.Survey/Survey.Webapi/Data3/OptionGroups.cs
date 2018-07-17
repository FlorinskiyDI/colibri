using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Webapi.Data3
{
    public partial class OptionGroups
    {
        public OptionGroups()
        {
            OptionChoises = new HashSet<OptionChoises>();
            Questions = new HashSet<Questions>();
        }

        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("OptionGroup")]
        public ICollection<OptionChoises> OptionChoises { get; set; }
        [InverseProperty("OptionGroup")]
        public ICollection<Questions> Questions { get; set; }
    }
}
