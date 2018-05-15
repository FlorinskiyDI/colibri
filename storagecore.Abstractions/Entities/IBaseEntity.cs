using System;
using System.Collections.Generic;
using System.Text;

namespace storagecore.Abstractions.Entities
{
    public interface IBaseEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
