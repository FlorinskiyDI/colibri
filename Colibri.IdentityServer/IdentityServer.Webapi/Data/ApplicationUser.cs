using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            ApplicationUserGroups = new HashSet<ApplicationUserGroups>();
        }

        public bool IsAdmin { get; set; }

        public string DataEventRecordsRole { get; set; }
        public string SecuredFilesRole { get; set; }
        public DateTime AccountExpires { get; set; }

        public double? EmailConfirmTokenLifespan { get; set; }
        public DateTimeOffset? EmailConfirmInvitationDate { get; set; }

        [InverseProperty("User")]
        public ICollection<ApplicationUserGroups> ApplicationUserGroups { get; set; }
        [InverseProperty("User")]
        public ICollection<MemberGroups> MemberGroups { get; set; }

        //public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
        //public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
        //public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }
        //public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
