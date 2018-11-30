using dataaccesscore.EFCore.Models;
using dataaccesscore.EFCore.Repositories;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Webapi.Repositories
{
    public class MemberGroupRepository : BaseRepository<ApplicationDbContext, MemberGroups>, IMemberGroupRepository
    {
        public MemberGroupRepository(ILogger<LoggerDataAccess> logger)
        : base(logger, null)
        {
        }
    }
}
