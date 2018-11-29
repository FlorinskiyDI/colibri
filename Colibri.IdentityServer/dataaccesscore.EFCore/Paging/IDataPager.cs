using dataaccesscore.Abstractions.Entities;
using dataaccesscore.EFCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataaccesscore.EFCore.Paging
{
    public interface IDataPager<TEntity, TKey>
            where TEntity : IBaseEntity<TKey>
    {
        DataPage<TEntity> Get(
            int pageNumber,
            int pageLength,
            OrderBy<TEntity> orderby = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );
        DataPage<TEntity> Query(
            int pageNumber,
            int pageLength,
            Filter<TEntity> filter,
            OrderBy<TEntity> orderby = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );

        Task<DataPage<TEntity>> GetAsync(
            int pageNumber,
            int pageLength,
            OrderBy<TEntity> orderby = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );
        Task<DataPage<TEntity>> QueryAsync(
            int pageNumber,
            int pageLength,
            Filter<TEntity> filter,
            OrderBy<TEntity> orderby = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
    }
}
