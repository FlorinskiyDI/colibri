using System;
using System.Collections.Generic;
using System.Text;

namespace dataaccesscore.Abstractions.Entities
{
    public interface IBaseEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
