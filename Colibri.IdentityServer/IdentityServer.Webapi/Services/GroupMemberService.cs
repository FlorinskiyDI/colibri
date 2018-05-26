using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer.Webapi.Services.Interfaces;

namespace IdentityServer.Webapi.Services
{
    public class GroupMemberService : IGroupMemberService
    {
        public Task<bool> AddMembersToGroupAsync(Guid groupId, List<string> emailList)
        {
            throw new NotImplementedException();
        }
    }
}
