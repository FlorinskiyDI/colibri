using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Data
{
    public class GroupNode
    {
        public Guid AncestorId { get; set; }
        public virtual Groups Ancestor { get; set; }

        public Guid OffspringId { get; set; }
        public virtual Groups Offspring { get; set; }

        public int Depth { get; set; }
    }
}
