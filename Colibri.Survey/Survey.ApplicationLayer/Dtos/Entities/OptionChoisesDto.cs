using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Entities
{
    public class OptionChoisesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OptionGroupId { get; set; }
        public int OrderNo { get; set; }
    }
}
