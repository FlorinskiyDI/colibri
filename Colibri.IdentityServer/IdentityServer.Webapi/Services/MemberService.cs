using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dataaccesscore.Abstractions.Uow;
using dataaccesscore.EFCore.Query;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos;
using IdentityServer.Webapi.Dtos.Search;
using IdentityServer.Webapi.Services.Interfaces;

namespace IdentityServer.Webapi.Services
{
    public class MemberService : IMemberService
    {
        //private readonly IAppUserService _appUserService;
        protected readonly IUowProvider _uowProvider;
        public MemberService(
            //IAppUserService appUserService
            IUowProvider uowProvider
        ) {
            //_appUserService = appUserService;
            _uowProvider = uowProvider;
        }

        public async Task<SearchResult<MemberDto>> GetMembersByGroup(string groupId, SearchQuery searchEntry)
        {
            // generate sort expression
            var sort = searchEntry?.OrderStatement == null
                ? new OrderBy<ApplicationUser>(c => c.OrderBy(d => d.Id))
                : new OrderBy<ApplicationUser>(searchEntry.OrderStatement.ColumName, searchEntry.OrderStatement.Reverse);
            // generate filter expression
            var filters = new Filter<ApplicationUser>(c => c.MemberGroups.Any(d => d.GroupId == new Guid(groupId)));

            var page = new SearchResult<MemberDto>();
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    var repository = uow.GetRepository<ApplicationUser>();

                    // get data
                    if (searchEntry?.SearchQueryPage == null)
                    {
                        var data = await repository.QueryAsync(filters.Expression, sort.Expression);
                        page = new SearchResult<MemberDto>()
                        {
                            ItemList = data.Select(c => new MemberDto
                            {
                                Id = c.Id,
                                UserName = c.UserName,
                                Email = c.Email,
                                EmailConfirmed = c.EmailConfirmed
                            }).ToList()
                        };
                    }
                    // get page data
                    else
                    {
                        var startRow = searchEntry.SearchQueryPage.PageNumber;
                        var data = await repository.QueryPageAsync(startRow, searchEntry.SearchQueryPage.PageLength, filters.Expression, sort.Expression);
                        var totalCount = await repository.CountAsync(filters.Expression);

                        page = new SearchResult<MemberDto>()
                        {
                            ItemList = data.Select(c => new MemberDto
                            {
                                Id = c.Id,
                                UserName = c.UserName,
                                Email = c.Email,
                                EmailConfirmed = c.EmailConfirmed
                            }).ToList(),
                            SearchResultPage = new SearchResultPage()
                            {
                                TotalItemCount = totalCount,
                                PageLength = searchEntry.SearchQueryPage.PageLength,
                                PageNumber = totalCount / searchEntry.SearchQueryPage.PageLength
                            }
                        };
                    }
                }
            }
            catch (Exception ex) { throw ex; }

            return page;
        }


        //public async Task<bool> AddMembersToGroupAsync(Guid groupId, List<string> emailList)
        //{
        //    foreach (var email in emailList)
        //    {
        //        var user = await _appUserService.AddUserByEmailWithoutPassword(email);
        //        //_groupServices.SubscribeToGroupAsync(user.Id, groupId);
        //    }
        //    //
        //    return true;
        //}

        //public async Task<IEnumerable<ApplicationUser>> GetMembersForGroupAsync(Guid groupId)
        //{
        //    var list = await _appUserRepository.GetAppUsersForGroup(groupId);
        //    //
        //    return list;
        //}

        //public async Task DeleteMember(string userId, Guid groupId)
        //{
        //    //await _groupServices.UnsubscribeToGroup(userId, groupId);
        //    return;
        //}
    }
}
