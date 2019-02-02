using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Data
{
    public class MemberGroups
    {
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public DateTimeOffset DateOfSubscribe { get; set; }

        [ForeignKey("GroupId")]
        [InverseProperty("MemberGroups")]
        public Groups Group { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("MemberGroups")]
        public ApplicationUser User { get; set; }
    }
}
