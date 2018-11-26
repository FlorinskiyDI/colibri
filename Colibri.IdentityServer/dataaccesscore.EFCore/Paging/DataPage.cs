using dataaccesscore.Abstractions.Entities;
using System;
using System.Collections.Generic;

namespace dataaccesscore.EFCore.Paging
{
    public class DataPage<TEntity>
    {
        public IEnumerable<TEntity> Items { get; set; }

        public long TotalItemCount { get; set; }
        public int TotalPageCount => Convert.ToInt32(Math.Ceiling((decimal)TotalItemCount / PageLength));

        public int PageNumber { get; set; }
        public int PageLength { get; set; }

    }
}
