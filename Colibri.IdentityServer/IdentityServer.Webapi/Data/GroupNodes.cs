using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Data
{
    public partial class GroupNodes
    {
        public Guid AncestorId { get; set; }
        public Guid OffspringId { get; set; }
        public int? Separation { get; set; }

        [ForeignKey("AncestorId")]
        [InverseProperty("GroupNodesAncestor")]
        public Groups Ancestor { get; set; }
        [ForeignKey("OffspringId")]
        [InverseProperty("GroupNodesOffspring")]
        public Groups Offspring { get; set; }
    }
}
