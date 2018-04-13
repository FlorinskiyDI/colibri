using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ApplicationUserGroups = new HashSet<ApplicationUserGroups>();
        }

        public bool IsAdmin { get; set; }
        public string DataEventRecordsRole { get; set; }
        public string SecuredFilesRole { get; set; }
        public DateTime AccountExpires { get; set; }

        [InverseProperty("User")]
        public ICollection<ApplicationUserGroups> ApplicationUserGroups { get; set; }
    }
}
