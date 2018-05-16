using Survey.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.InfrastructureLayer.IdentityServices
{
    public interface IGroupRequestService
    {
        Task<IEnumerable<Groups>> GetGroupList();
        Task<IEnumerable<Groups>> GetSubGroupList(Guid groupId);
    }
}
