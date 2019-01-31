using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Dtos
{
    public class GroupPolicyDto
    {
        public Guid GroupId { get; set; }
        public List<string> Emails { get; set; }
        public List<string> Roles { get; set; }
    }
}
