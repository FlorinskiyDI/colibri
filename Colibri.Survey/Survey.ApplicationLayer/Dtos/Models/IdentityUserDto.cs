using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models
{
    public class IdentityUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
