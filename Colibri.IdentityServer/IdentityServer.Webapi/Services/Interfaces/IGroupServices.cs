using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services.Interfaces
{
    public interface IGroupServices
    {
        void SubscribeToGroupAsync(string userId, Guid groupId);
        Task UnsubscribeToGroup(string userId, Guid groupId);
    }
}
