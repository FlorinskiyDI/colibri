﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Data
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        [Key]
        [InverseProperty("ApplicationUserRoles")]
        public Groups Group { get; set; }
        public Guid GroupId { get; set; }
    }
}
