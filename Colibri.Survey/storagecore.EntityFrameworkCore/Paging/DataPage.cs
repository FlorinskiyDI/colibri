using storagecore.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace storagecore.EntityFrameworkCore.Paging
{
    public class DataPage<TEntity, TKey>
        where TEntity : IBaseEntity<TKey>
    {
        public IEnumerable<TEntity> Items { get; set; }

        public long TotalItemCount { get; set; }
        public int TotalPageCount => Convert.ToInt32(Math.Ceiling((decimal)TotalItemCount / PageLength));

        public int PageNumber { get; set; }
        public int PageLength { get; set; }

    }
}
