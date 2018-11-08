using dataaccesscore.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.DomainModelLayer.Models.IdentityServer.Pager
{
    public class PageDataModel<TEntity>
        where TEntity : class
    {
        public IEnumerable<TEntity> Items { get; set; }

        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }

        public int PageNumber { get; set; }
        public int PageLength { get; set; }
    }
}
