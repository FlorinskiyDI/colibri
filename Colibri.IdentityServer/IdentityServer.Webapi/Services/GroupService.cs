using dataaccesscore.Abstractions.Uow;
using dataaccesscore.EFCore.Paging;
using dataaccesscore.EFCore.Query;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos;
using IdentityServer.Webapi.Dtos.Search;
//using IdentityServer.Webapi.Dtos.Search;
using IdentityServer.Webapi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services
{
    public class GroupService : IGroupService
    {

        private readonly IDataPager<Groups, Guid> _pager;
        protected readonly IUowProvider _uowProvider;

        public GroupService(
            IUowProvider uowProvider,
            IDataPager<Groups, Guid> pager
        )
        {
            _uowProvider = uowProvider;
            _pager = pager;
        }

        public async Task<SearchResult<GroupDto>> GetGroupsAsync(string userId, SearchQuery searchEntry, bool isRoot = false)
        {
            // generate sort expression
            var sort = searchEntry.OrderStatement == null
                ? new OrderBy<Groups>(c => c.OrderBy(d => d.Name))
                : new OrderBy<Groups>(searchEntry.OrderStatement.ColumName, searchEntry.OrderStatement.Reverse);
            // generate filter expression
            var filters = new Filter<Groups>(c => c.ApplicationUserGroups.Any(d => d.UserId == userId));
            // generate includes expression
            var includes = new Includes<Groups>(c => c.Include(v => v.InverseParent).Include(v => v.Parent));

            var page = new SearchResult<GroupDto>();
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    var repository = uow.GetRepository<Groups, Guid>();
                    // if items are root, than get parentids and add new filter
                    if (isRoot)
                    {
                        var items = repository.Query(c => c.ApplicationUserGroups.Any(d => d.UserId == userId)).Select(c => new { Id = c.Id, ParentId = c.ParentId }).ToList();
                        var itemParentIds = items.Where(c => c.ParentId != null).Select(c => c.ParentId).ToList();
                        var itemIds = items.Select(c => c.Id).ToList();
                        var itemsUnion = itemParentIds.Where(c => !itemIds.Contains(c.Value)).ToList();
                        filters.AddExpression(c => itemsUnion.Contains(c.ParentId.Value) || c.ParentId == null);  // TODO: List "itemsParentIds" can store a large list that will be passed in the request. It may cause an error in the future!!!
                    }

                    // get data
                    if (searchEntry.SearchQueryPage == null)
                    {
                        var data = await repository.QueryAsync(filters.Expression, sort.Expression, includes.Expression);
                        page = new SearchResult<GroupDto>()
                        {
                            ItemList = data.Select(c => new GroupDto
                            {
                                Id = c.Id,
                                ParentId = c.ParentId,
                                Name = c.Name,
                                CountChildren = c.InverseParent.Count
                            }).ToList(),
                        };

                    }
                    // get page data
                    else
                    {
                        var startRow = searchEntry.SearchQueryPage.PageNumber;
                        var data = await repository.QueryPageAsync(startRow, searchEntry.SearchQueryPage.PageLength, filters.Expression, sort.Expression, includes.Expression);
                        var totalCount = await repository.CountAsync(filters.Expression);

                        page = new SearchResult<GroupDto>()
                        {
                            ItemList = data.Select(c => new GroupDto
                            {
                                Id = c.Id,
                                ParentId = c.ParentId,
                                Name = c.Name,
                                CountChildren = c.InverseParent.Count
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

        public async Task<IEnumerable<GroupDto>> GetByParentIdAsync(string userId, SearchQuery searchEntry, string parentId)
        {
            // generate sort expression
            var sort = searchEntry.OrderStatement == null
                ? new OrderBy<Groups>(c => c.OrderBy(d => d.Name))
                : new OrderBy<Groups>(searchEntry.OrderStatement.ColumName, searchEntry.OrderStatement.Reverse);
            // generate filter expression
            var filters = new Filter<Groups>(c => c.ApplicationUserGroups.Any(d => d.UserId == userId));
            filters.AddExpression(c => c.ParentId == new Guid(parentId));
            // generate includes expression
            var includes = new Includes<Groups>(c => c.Include(v => v.InverseParent).Include(v => v.Parent));

            IEnumerable<GroupDto> data;
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    var repository = uow.GetRepository<Groups, Guid>();

                    // get data
                    var items = await repository.QueryAsync(filters.Expression, sort.Expression, includes.Expression);
                    data = items.Select(c => new GroupDto
                    {
                        Id = c.Id,
                        ParentId = c.ParentId,
                        Name = c.Name,
                        CountChildren = c.InverseParent.Count
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }



        //public async Task<DataPage<Groups, Guid>> GetRootAsync(PageSearchEntry searchEntry, string userId)
        //{
        //    var pageData = new PageData<Groups>();
        //    // sort
        //    var sort = new OrderBy<Groups>(c => c.OrderBy(d => d.Name)); // default order
        //    if (searchEntry.OrderStatement != null)
        //    {
        //        sort = new OrderBy<Groups>(searchEntry.OrderStatement.ColumName, searchEntry.OrderStatement.Reverse);
        //    }
        //    // init filter
        //    var filters = new Filter<Groups>(c => c.ApplicationUserGroups.Any(d =>d.UserId == userId));
        //    if (searchEntry.FilterStatements.Count() > 0)
        //    { }
        //    try
        //    {
        //        var results = await _pager.QueryAsync(
        //            searchEntry.PageNumber, // PageNumber should be more than 0!!!
        //            searchEntry.PageLength,
        //            filters,
        //            sort
        //            ); 
        //        return results;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public async void SubscribeToGroupAsync(string userId, Guid groupId)
        //{
        //    var userGroup = await _appUserGroupRepository.GetAppUserGroupAsync(userId, groupId);
        //    if (userGroup == null)
        //    {
        //        await _appUserGroupRepository.CreateAppUserGroupAsync(new ApplicationUserGroups()
        //        {
        //            GroupId = groupId,
        //            UserId = userId
        //        });
        //    }
        //}

        //public async Task UnsubscribeToGroup(string userId, Guid groupId)
        //{
        //    var userGroup = await _appUserGroupRepository.GetAppUserGroupAsync(userId, groupId);
        //    if (userGroup == null)
        //    {
        //        throw new ArgumentException("not found userGroup");
        //    }
        //    _appUserGroupRepository.DeleteAppUserGroupAsync(userGroup);

        //    return;
        //}
        //}
    }
}
