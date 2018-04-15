using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Webapi.Data3
{
    public partial class ApplicationUserGroups
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        [Required]
        public string UserId { get; set; }

        [ForeignKey("GroupId")]
        [InverseProperty("ApplicationUserGroups")]
        public Groups Group { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("ApplicationUserGroups")]
        public AspNetUsers User { get; set; }
    }
}
