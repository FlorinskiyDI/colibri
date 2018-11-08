using Survey.DomainModelLayer.Models;
using Survey.DomainModelLayer.Models.IdentityServer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey.InfrastructureLayer.IdentityServerServices.Interfaces
{
    public interface IIdentityUserRequestService
    {
        Task<IEnumerable<IdentityUserModel>> GetIdentityUsersAsync(Guid groupId);
    }
}
