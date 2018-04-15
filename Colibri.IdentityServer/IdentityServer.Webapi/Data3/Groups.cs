using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer.Webapi.Data3
{
    public partial class Groups
    {
        public Groups()
        {
            ApplicationUserGroups = new HashSet<ApplicationUserGroups>();
            GroupNodesAncestor = new HashSet<GroupNodes>();
            GroupNodesOffspring = new HashSet<GroupNodes>();
            InverseParent = new HashSet<Groups>();
        }

        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        [InverseProperty("InverseParent")]
        public Groups Parent { get; set; }
        [InverseProperty("Group")]
        public ICollection<ApplicationUserGroups> ApplicationUserGroups { get; set; }
        [InverseProperty("Ancestor")]
        public ICollection<GroupNodes> GroupNodesAncestor { get; set; }
        [InverseProperty("Offspring")]
        public ICollection<GroupNodes> GroupNodesOffspring { get; set; }
        [InverseProperty("Parent")]
        public ICollection<Groups> InverseParent { get; set; }
    }
}
