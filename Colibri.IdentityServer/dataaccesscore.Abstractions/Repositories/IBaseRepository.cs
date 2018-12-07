using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dataaccesscore.Abstractions.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        // get all
        IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );
        Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );

        // get page
        IEnumerable<TEntity> GetPage(
            int startRij,
            int aantal,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );
        Task<IEnumerable<TEntity>> GetPageAsync(
            int startRij,
            int aantal,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );

        // get
        TEntity Get<TKey>(
            TKey id,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );
        Task<TEntity> GetAsync<TKey>(
            TKey id,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );

        // query
        IEnumerable<TEntity> Query(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );
        Task<IEnumerable<TEntity>> QueryAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );

        // query page
        IEnumerable<TEntity> QueryPage(
            int startRij,
            int aantal,
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );
        Task<IEnumerable<TEntity>> QueryPageAsync(
            int startRij,
            int aantal,
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );

        // load
        void Load(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );
        Task LoadAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        );

        // add
        void Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);


        //add range
        Task AddRangeAsync(IEnumerable<TEntity> entitys);
        void AddRange(IEnumerable<TEntity> entitys);
        // update
        TEntity Update(TEntity entity);

        // remove
        void Remove(TEntity entity);
        void Remove<TKey>(TKey id);
        void RemoveRange(IEnumerable<TEntity> list);

        // any
        bool Any(Expression<Func<TEntity, bool>> filter = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null);

        // count
        int Count(Expression<Func<TEntity, bool>> filter = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);

        // set unchanged
        void SetUnchanged(TEntity entitieit);
    }
}
