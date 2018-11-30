using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.DomainModelLayer.Models.IdentityServer
{
    public class MemberModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
