using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Webapi.Data3
{
    public partial class InputTypes
    {
        public InputTypes()
        {
            Questions = new HashSet<Questions>();
        }

        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("InputTypes")]
        public ICollection<Questions> Questions { get; set; }
    }
}
