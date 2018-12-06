using dataaccesscore.Abstractions.Uow;
using dataaccesscore.EFCore.Query;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services
{
    public class GroupNodeService : IGroupNodeService
    {

        protected readonly IUowProvider _uowProvider;
        public GroupNodeService(
            IUowProvider uowProvider
        )
        {
            _uowProvider = uowProvider;
        }

        public async Task<IEnumerable<GroupNode>> GetAncestors(Guid descendantId)
        {
            IEnumerable<GroupNode> list;
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    var repository = uow.GetRepository<GroupNode>();
                    list = await repository.QueryAsync(c => c.OffspringId == descendantId);
                    await uow.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        public async Task AddPathsBetweenDescendantAndAncestors(Guid newDescendantId, Guid? groupId = null)
        {
            List<GroupNode> list = new List<GroupNode>();
            list.Add(new GroupNode { AncestorId = newDescendantId, OffspringId = newDescendantId, Depth = 0 }); // 

            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    var repository = uow.GetRepository<GroupNode>();

                    // if was set parent, then get all ancestors for parent and add they to path list
                    if (groupId != null)
                    {
                        var ancestorListForNewDescendant = await repository.QueryAsync(c => c.OffspringId == groupId);
                        foreach (var item in ancestorListForNewDescendant)
                        {
                            list.Add(new GroupNode
                            {
                                AncestorId = item.AncestorId,
                                OffspringId = newDescendantId,
                                Depth = 0
                            });
                        }
                    }

                    await repository.AddRangeAsync(list.ToArray());
                    await uow.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }

        public async Task DeletePathsForAncestorsByDescendants(IEnumerable<GroupNode> ancestorList, IEnumerable<GroupNode> descendantList)
        {
            var ancestorIds = ancestorList.Select(c =>c.AncestorId).ToList();
            var descendantIds = descendantList.Select(c => c.OffspringId).ToList();
            var includes = new Includes<GroupNode>(c => c.Where(d => ancestorIds.Contains(d.AncestorId) && descendantIds.Contains(d.OffspringId)));

            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    var repository = uow.GetRepository<GroupNode>();
                    var data = await repository.GetAllAsync(includes: includes.Expression);
                    repository.RemoveRange(data);
                    await uow.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return;
        }
    }
}
