using dataaccesscore.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace dataaccesscore.EFCore.Entities
{
    public class BaseEntity<TKey> : IBaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }

    public class BaseTenantEntity<TKey> : BaseEntity<TKey>, IBaseEntity<TKey>
    {
        public TKey TenantId { get; set; }
    }
}
