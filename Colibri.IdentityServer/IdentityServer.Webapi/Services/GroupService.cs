using AutoMapper;
using dataaccesscore.Abstractions.Uow;
using dataaccesscore.EFCore.Paging;
using dataaccesscore.EFCore.Query;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos;
using IdentityServer.Webapi.Dtos.Search;
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
        private readonly IUserGroupService _userGroupService;
        private readonly IGroupNodeService _groupNodeService;
        private readonly IDataPager<Groups> _pager;
        protected readonly IUowProvider _uowProvider;
        protected readonly IMapper _mapper;

        public GroupService(
            IUowProvider uowProvider,
            IDataPager<Groups> pager,
            IGroupNodeService groupNodeService,
            IUserGroupService userGroupService,
            IMapper mapper
        )
        {
            _uowProvider = uowProvider;
            _pager = pager;
            _mapper = mapper;
            _groupNodeService = groupNodeService;
            _userGroupService = userGroupService;
        }

        public async Task<SearchResult<GroupDto>> GetRootAsync(string userId, SearchQuery searchEntry)
        {
            // generate sort expression
            var sort = searchEntry?.OrderStatement == null
                ? new OrderBy<Groups>(c => c.OrderBy(d => d.Name))
                : new OrderBy<Groups>(searchEntry.OrderStatement.ColumName, searchEntry.OrderStatement.Reverse);
            // generate filter expression
            var filters = new Filter<Groups>(c => c.ApplicationUserGroups.Any(d => d.UserId == userId));

            var page = new SearchResult<GroupDto>();
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    var repository = uow.GetRepository<Groups>();

                    // get data
                    if (searchEntry?.SearchQueryPage == null)
                    {
                        var data = await repository.QueryAsync(filters.Expression, sort.Expression);
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
                        var data = await repository.QueryPageAsync(startRow, searchEntry.SearchQueryPage.PageLength, filters.Expression, sort.Expression);
                        var totalCount = await repository.CountAsync(filters.Expression);

                        page = new SearchResult<GroupDto>()
                        {
                            ItemList = data.Select(c => new GroupDto
                            {
                                Id = c.Id,
                                ParentId = c.ParentId,
                                Name = c.Name,
                                CountChildren = c.InverseParent.Count,
                                GroupID = c.GroupID,
                                Description = c.Description
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

        public async Task<SearchResult<GroupDto>> GetAllAsync(string userId, SearchQuery searchEntry)
        {
            // generate sort expression
            var sort = searchEntry?.OrderStatement == null
                ? new OrderBy<Groups>(c => c.OrderBy(d => d.Name))
                : new OrderBy<Groups>(searchEntry.OrderStatement.ColumName, searchEntry.OrderStatement.Reverse);
            // generate includes expression
            var includes = new Includes<Groups>(c => c.Include(v => v.InverseParent).Include(v => v.Parent));

            var page = new SearchResult<GroupDto>();
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    var repository = uow.GetRepository<Groups>();

                    // get root groups fo user
                    var items = repository.Query(c => c.ApplicationUserGroups.Any(d => d.UserId == userId)).ToList();
                    Filter<Groups> filters = new Filter<Groups>(c => c.Ancestors.Any(d => items.Contains(d.Ancestor)));  // TODO: List "itemsParentIds" can store a large list that will be passed in the request. It may cause an error in the future!!!
                    //Filter<Groups> filters = new Filter<Groups>(c => items.Contains(c.ParentId.Value) || c.ParentId == null);  // TODO: List "itemsParentIds" can store a large list that will be passed in the request. It may cause an error in the future!!!

                    // get data
                    if (searchEntry?.SearchQueryPage == null)
                    {
                        var data = await repository.QueryAsync(filters.Expression, sort.Expression);
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
                        var data = await repository.QueryPageAsync(startRow, searchEntry.SearchQueryPage.PageLength, filters.Expression, sort.Expression);
                        var totalCount = await repository.CountAsync(filters.Expression);

                        page = new SearchResult<GroupDto>()
                        {
                            ItemList = data.Select(c => new GroupDto
                            {
                                Id = c.Id,
                                ParentId = c.ParentId,
                                Name = c.Name,
                                CountChildren = c.InverseParent.Count,
                                GroupID = c.GroupID,
                                Description = c.Description
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
            var sort = searchEntry?.OrderStatement == null
                ? new OrderBy<Groups>(c => c.OrderBy(d => d.Name))
                : new OrderBy<Groups>(searchEntry.OrderStatement.ColumName, searchEntry.OrderStatement.Reverse);
            // generate filter expression
            var filters = new Filter<Groups>(c => c.ParentId == new Guid(parentId));
            // generate includes expression
            var includes = new Includes<Groups>(c => c.Include(v => v.InverseParent).Include(v => v.Parent));

            IEnumerable<GroupDto> data;
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    var repository = uow.GetRepository<Groups>();

                    // get data
                    var items = await repository.QueryAsync(filters.Expression, sort.Expression, includes.Expression);
                    data = items.Select(c => new GroupDto
                    {
                        Id = c.Id,
                        ParentId = c.ParentId,
                        Name = c.Name,
                        CountChildren = c.InverseParent.Count,
                        GroupID = c.GroupID,
                        Description = c.Description
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }

        public async Task<GroupDto> CreateGroup(GroupDto model, string userId)
        {
            var entity = _mapper.Map<GroupDto, Groups>(model);
            try
            {
                // add new group
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    var repository = uow.GetRepository<Groups>();
                    var result = await repository.AddAsync(entity);
                    await uow.SaveChangesAsync();
                }
                // add user to group
                if (model.ParentId == null)
                {
                    await _userGroupService.AddUserToGroup(new ApplicationUserGroups { GroupId = entity.Id, UserId = userId });
                }
                // add paths between new descendant and exist ancestors
                await _groupNodeService.AddPathsBetweenDescendantAndAncestors(entity.Id, model.ParentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _mapper.Map<Groups, GroupDto>(entity);
        }

        public async Task DeleteGroup(string groupId, string userId)
        {
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    var repository = uow.GetRepository<Groups>();
                    var result = await repository.GetAsync<Guid>(new Guid(groupId), c => c.Include(d => d.Ancestors).Include(d => d.Offspring).Include(d => d.InverseParent));
                    // delete all paths to group
                    await _groupNodeService.DeletePathsForAncestorsByDescendants(result.Ancestors, result.Offspring);
                    await _userGroupService.DeletePathsWhereGroup(groupId);
                    repository.Remove(result);
                    await uow.SaveChangesAsync();

                    foreach (var item in result.InverseParent)
                    {
                        await _userGroupService.AddUserToGroup(new ApplicationUserGroups { GroupId = item.Id, UserId = userId });
                    }
                }

                // add new group
                //using (var uow = _uowProvider.CreateUnitOfWork())
                //{
                //    var repository = uow.GetRepository<Groups>();
                //    var result = await repository.AddAsync(entity);
                //    await uow.SaveChangesAsync();
                //}
                //// add user to group
                //if (model.ParentId == null)
                //{
                //    await _userGroupService.AddUserToGroup(new ApplicationUserGroups { GroupId = entity.Id, UserId = userId });
                //}
                //// add paths between new descendant and exist ancestors
                //await _groupNodeService.AddPathsBetweenDescendantAndAncestors(entity.Id, model.ParentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return;
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
