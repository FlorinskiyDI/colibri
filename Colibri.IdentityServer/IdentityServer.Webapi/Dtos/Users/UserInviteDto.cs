using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Dtos.Users
{
    public class UserInviteDto
    {
        public List<string> Emails { get; set; }
        public List <string> Roles { get; set; }
        public string GroupId { get; set; }
    }
}
