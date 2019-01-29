using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos;
using IdentityServer.Webapi.Dtos.Search;
using IdentityServer.Webapi.Dtos.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> AddUserByEmailWithoutPassword(string email);
        Task SendInvitationByEmailConfirmationToken(string userId);
        Task<SearchResult<AppUserPageDto>> GetSearchData(SearchQuery searchEntry);
        Task<UserFullDetailsViewModel> GetUserFullDetails(string userId);
    }
}
