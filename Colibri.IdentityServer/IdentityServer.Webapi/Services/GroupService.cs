using dataaccesscore.Abstractions.Uow;
using dataaccesscore.EFCore.Paging;
using dataaccesscore.EFCore.Query;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos.Pager;
using IdentityServer.Webapi.Repositories.Interfaces;
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
        ) {
            _uowProvider = uowProvider;
            _pager = pager;
        }

        public async Task<DataPage<Groups, Guid>> GetPageDataAsync(string userId, PageSearchEntry searchEntry, bool isRoot = false)
        {
            // generate sort expression
            var sort = searchEntry.OrderStatement == null
                ? new OrderBy<Groups>(c => c.OrderBy(d => d.Name))
                : new OrderBy<Groups>(searchEntry.OrderStatement.ColumName, searchEntry.OrderStatement.Reverse );

            // generate filter expression
            var filters = new Filter<Groups>(c => c.ApplicationUserGroups.Any(d => d.UserId == userId));

            try
            {
                // if items are root, than get parentids and add new filter
                if (isRoot)
                {
                    using (var uow = _uowProvider.CreateUnitOfWork())
                    {
                        var repository = uow.GetRepository<Groups, Guid>();
                        //var itemParentIds = repository.Query(c => c.ApplicationUserGroups.Any(d => d.UserId == userId) && c.ParentId != null).Select(c => c.ParentId.Value).ToList();

                        var items = repository.Query(c => c.ApplicationUserGroups.Any(d => d.UserId == userId)).Select(c => new { Id = c.Id, ParentId = c.ParentId }).ToList();
                        var itemParentIds = items.Where(c => c.ParentId != null).Select(c => c.ParentId).ToList();
                        var itemIds = items.Select(c => c.Id).ToList();

                        var itemsUnion = itemParentIds.Where( c => !itemIds.Contains(c.Value));

                        //filters.AddExpression(c => !itemsParentIds.Contains(c.Id) && itemsParentIds.Any(d => d != c.Id));  // TODO: List "itemsParentIds" can store a large list that will be passed in the request. It may cause an error in the future!!!
                        filters.AddExpression(c => itemsUnion.Contains(c.ParentId.Value) || c.ParentId==null);  // TODO: List "itemsParentIds" can store a large list that will be passed in the request. It may cause an error in the future!!!
                    }

                }

                var result = await _pager.QueryAsync(searchEntry.PageNumber, searchEntry.PageLength, filters, sort, d => d.Include(v => v.InverseParent).Include(v => v.Parent));                
                return result;
            }
            catch (Exception ex)
            {
                throw  ex;
            }
        }

        public async Task<List<Groups>> GetByParentIdAsync(string userId, string parentId = null)
        {
            return null;
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
    }
}
