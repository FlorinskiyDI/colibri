using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Data
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public string DataEventRecordsRole { get; set; }
        public string SecuredFilesRole { get; set; }
    }
}
