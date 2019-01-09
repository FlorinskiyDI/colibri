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

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    }
}
