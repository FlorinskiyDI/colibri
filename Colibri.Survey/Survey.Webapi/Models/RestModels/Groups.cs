using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Webapi.Models.RestModels
{
    public partial class Groups
    {
        public Groups()
        {
            InverseParent = new List<Groups>();
        }

        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }

        public Groups Parent { get; set; }
        public List<Groups> InverseParent { get; set; }
    }
}
