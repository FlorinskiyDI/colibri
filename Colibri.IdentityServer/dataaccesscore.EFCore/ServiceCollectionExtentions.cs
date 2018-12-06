using dataaccesscore.Abstractions.Context;
using dataaccesscore.Abstractions.Repositories;
using dataaccesscore.Abstractions.Uow;
using dataaccesscore.EFCore.Paging;
using dataaccesscore.EFCore.Repositories;
using dataaccesscore.EFCore.Uow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace dataaccesscore.EFCore
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddStorageCoreDataAccess<TEntityContext>(this IServiceCollection services) where TEntityContext : DbContext, IEntityContext
        {
            RegisterStorageCoreDataAccess<TEntityContext>(services);
            return services;
        }

        private static void RegisterStorageCoreDataAccess<TEntityContext>(IServiceCollection services) where TEntityContext : DbContext, IEntityContext
        {
            services.TryAddScoped<IUowProvider, UowProvider>();
            services.TryAddTransient<IEntityContext, TEntityContext>();

            services.TryAddTransient(typeof(IBaseRepository<>), typeof(GenericRepository<>));
            services.TryAddTransient(typeof(IDataPager<>), typeof(DataPager<>));
        }
    }
}
