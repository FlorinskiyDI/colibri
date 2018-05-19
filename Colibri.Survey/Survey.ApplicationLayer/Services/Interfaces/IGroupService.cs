using Survey.ApplicationLayer.Dtos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> GetGroupList();
        Task<IEnumerable<GroupDto>> GetSubGroupList(Guid groupId);
        Task<GroupDto> CreateGroup(GroupDto groupDto);
        Task<bool> DeleteGroup(Guid groupId);
        Task<GroupDto> GetGroup(Guid groupId);
    }
}
