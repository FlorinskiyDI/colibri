using storagecore.Abstractions.Entities;
using storagecore.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storagecore.EntityFrameworkCore.Paging
{
    public interface IDataPager<TEntity, TKey>
        where TEntity : IBaseEntity<TKey>
    {
        DataPage<TEntity, TKey> Get(
            int pageNumber,
            int pageLength,
            OrderBy<TEntity> orderby = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );
        DataPage<TEntity, TKey> Query(
            int pageNumber,
            int pageLength,
            Filter<TEntity> filter,
            OrderBy<TEntity> orderby = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );

        Task<DataPage<TEntity, TKey>> GetAsync(
            int pageNumber,
            int pageLength,
            OrderBy<TEntity> orderby = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );
        Task<DataPage<TEntity, TKey>> QueryAsync(
            int pageNumber,
            int pageLength,
            Filter<TEntity> filter,
            OrderBy<TEntity> orderby = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
    }
}
