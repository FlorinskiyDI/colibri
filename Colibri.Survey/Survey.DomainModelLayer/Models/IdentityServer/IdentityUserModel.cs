using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.DomainModelLayer.Models.IdentityServer
{
    public class IdentityUserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
