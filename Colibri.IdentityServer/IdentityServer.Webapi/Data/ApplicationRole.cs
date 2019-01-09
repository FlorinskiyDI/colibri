using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace IdentityServer.Webapi.Data
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public ApplicationRole() : base()
        { }
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
