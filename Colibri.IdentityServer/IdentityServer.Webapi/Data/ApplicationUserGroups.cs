using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Data
{
    public partial class ApplicationUserGroups
    {
        public Guid Id { get; set; }
        [Required]       
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }

        [ForeignKey("GroupId")]
        [InverseProperty("ApplicationUserGroups")]
        public Groups Group { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("ApplicationUserGroups")]
        public ApplicationUser User { get; set; }
    }
}
