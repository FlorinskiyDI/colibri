using dataaccesscore.Abstractions.Uow;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services
{
    public class UserGroupService : IUserGroupService
    {

        protected readonly IUowProvider _uowProvider;
        public UserGroupService(
            IUowProvider uowProvider
        )
        {
            _uowProvider = uowProvider;
        }

        public async Task<ApplicationUserGroups> AddUserToGroup(ApplicationUserGroups model)
        {
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    var repository = uow.GetRepository<ApplicationUserGroups>();
                    var result = await repository.AddAsync(model);
                    await uow.SaveChangesAsync();

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
