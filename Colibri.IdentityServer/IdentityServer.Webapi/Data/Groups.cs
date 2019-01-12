using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Webapi.Data
{
    public partial class Groups
    {
        public Groups()
        {
            ApplicationUserGroups = new HashSet<ApplicationUserGroups>();
            InverseParent = new HashSet<Groups>();
        }
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string GroupID { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        [ForeignKey("ParentId")]
        [InverseProperty("InverseParent")]
        public Groups Parent { get; set; }
        [InverseProperty("Group")]
        public ICollection<ApplicationUserGroups> ApplicationUserGroups { get; set; }
        [InverseProperty("Group")]
        public ICollection<MemberGroups> MemberGroups { get; set; }
        [InverseProperty("Parent")]
        public ICollection<Groups> InverseParent { get; set; }
        [InverseProperty("Group")]
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public virtual ICollection<GroupNode> Ancestors { get; set; }
        public virtual ICollection<GroupNode> Offspring { get; set; }
    }
}
