using storagecore.Abstractions.Entities;
using storagecore.Abstractions.Uow;
using storagecore.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storagecore.EntityFrameworkCore.Paging
{
    public class DataPager<TEntity, TKey> : IDataPager<TEntity, TKey>
        where TEntity : IBaseEntity<TKey>
    {
        public DataPager(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }

        private readonly IUowProvider _uowProvider;

        public DataPage<TEntity, TKey> Get(
            int pageNumber,
            int pageLength,
            OrderBy<TEntity> orderby = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        )
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<TEntity, TKey>();

                var startRow = (pageNumber - 1) * pageLength;
                var data = repository.GetPage(startRow, pageLength, includes: includes, orderBy: orderby?.Expression);
                var totalCount = repository.Count();

                return CreateDataPage(pageNumber, pageLength, data, totalCount);
            }
        }

        public async Task<DataPage<TEntity, TKey>> GetAsync(
            int pageNumber,
            int pageLength,
            OrderBy<TEntity> orderby = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        )
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<TEntity, TKey>();

                var startRow = (pageNumber - 1) * pageLength;
                var data = await repository.GetPageAsync(startRow, pageLength, includes: includes, orderBy: orderby?.Expression);
                var totalCount = await repository.CountAsync();

                return CreateDataPage(pageNumber, pageLength, data, totalCount);
            }
        }

        public DataPage<TEntity, TKey> Query(
            int pageNumber,
            int pageLength,
            Filter<TEntity> filter,
            OrderBy<TEntity> orderby = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        )
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<TEntity, TKey>();

                var startRow = (pageNumber - 1) * pageLength;
                var data = repository.QueryPage(startRow, pageLength, filter.Expression, includes: includes, orderBy: orderby?.Expression);
                var totalCount = repository.Count(filter.Expression);

                return CreateDataPage(pageNumber, pageLength, data, totalCount);
            }
        }

        public async Task<DataPage<TEntity, TKey>> QueryAsync(
            int pageNumber,
            int pageLength,
            Filter<TEntity> filter,
            OrderBy<TEntity> orderby = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null
        )
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<TEntity, TKey>();

                var startRow = (pageNumber - 1) * pageLength;
                var data = await repository.QueryPageAsync(startRow, pageLength, filter.Expression, includes: includes, orderBy: orderby?.Expression);
                var totalCount = await repository.CountAsync(filter.Expression);

                return CreateDataPage(pageNumber, pageLength, data, totalCount);
            }
        }

        private DataPage<TEntity, TKey> CreateDataPage(
            int pageNumber,
            int pageLength,
            IEnumerable<TEntity> data,
            long totalEntityCount
        )
        {
            var page = new DataPage<TEntity, TKey>()
            {
                Items = data,
                TotalItemCount = totalEntityCount,
                PageLength = pageLength,
                PageNumber = pageNumber
            };

            return page;
        }
    }
}
