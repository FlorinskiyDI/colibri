using dataaccesscore.EFCore.Models;
using dataaccesscore.EFCore.Repositories;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos.Views;
using IdentityServer.Webapi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories
{
    public class AppUserRepository : BaseRepository<ApplicationDbContext, Groups>, IAppUserRepository
    {
        public AppUserRepository(ILogger<LoggerDataAccess> logger)
        : base(logger, null)
        {
        }

        public async Task<IEnumerable<ApplicationUser>> GetAppUsersForGroup(Guid groupId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                return await ctx.Set<ApplicationUserGroups>()
                    .Where(c => c.GroupId == groupId)
                    .Select(c => c.User)
                    .ToListAsync();
            }
        }
        [AllowAnonymous]
        public async Task<UserFullDetailsViewModel> GetUserFullDetails(string userId)
        {
            UserFullDetailsViewModel result = new UserFullDetailsViewModel();

            using (var ctx = new ApplicationDbContext())
            {
                var UserRepository = ctx.Set<ApplicationUser>();
                var A_UGroupRepository = ctx.Set<ApplicationUserGroups>();
                var MemberGroupRepository = ctx.Set<MemberGroups>();
                var GroupRepository = ctx.Set<Groups>();


                //var check6 = UserRepository.Where(x => x.Id == userId)

                //.Join(MemberGroupRepository,
                //    user => user.Id,
                //    member => member.UserId,
                //    (user, member) => new { user, member })

                // .Join(GroupRepository,
                //    y => y.member.GroupId,
                //    group1 => group1.Id,
                //    (y, group1) => new { y, group1 })

                // .Join(A_UGroupRepository,
                //    l => l.group1.Id,
                //    group2 => group2.GroupId,
                //    (l, group2) => new { groupId = l.group1, userId = l.y.user.Id })
                //  .ToList();



                //var check5 = UserRepository.Where(x => x.Id == userId)
                //    .Join(A_UGroupRepository,
                //    user => user.Id,
                //    a_u => a_u.UserId,
                //    (user, a_u) => new { user, a_u })
                //    .Join(MemberGroupRepository,
                //    x => x.user.Id,
                //    member => member.UserId,
                //    (x, member) => new { x, member })
                //    .Join(GroupRepository,
                //    y => y.member.GroupId,
                //    group1 => group1.Id,
                //    (y, group1) => new { memId = y.member.Id, groupId = group1.Id, userId = y.x.user.Id })

                //    .ToList();



                //var check = A_UGroupRepository.Where(x => x.UserId == userId)
                //     .Join(GroupRepository,
                //        a_u => a_u.GroupId,
                //        g => g.Id,
                //        (u, g) => new { u, g })
                //     .Join(MemberGroupRepository,
                //        k => k.g.Id,
                //        mem => mem.GroupId,
                //        (k, mem) => new { userId = k.u.UserId, groupId = k.g.Id }
                //        ).ToList();

                //var res1 = await A_UGroupRepository.Where(x => x.UserId == userId)
                //    .Join(GroupRepository,
                //        a_u => a_u.GroupId,
                //        g => g.Id,
                //        (u, g) => g)
                //     .ToListAsync();
                //var res2 = await MemberGroupRepository.Where(x => x.UserId == userId)
                //.Join(GroupRepository,
                //    u => u.GroupId,
                //    g => g.Id,
                //    (u, g) => g)
                // .ToListAsync();
                //var result2 = UserRepository.Where(x => x.Id == userId).SingleOrDefault();

                //var res1 = await A_UGroupRepository.Where(x => x.UserId == userId)
                //.Join(GroupRepository,
                //    a_u => a_u.GroupId,
                //    g => g.Id,
                //    (u, g) =>  g )
                // .ToListAsync();
                //var res2 = await MemberGroupRepository.Where(x => x.UserId == userId)
                //.Join(GroupRepository,
                //    u => u.GroupId,
                //    g => g.Id,
                //    (u, g) => g)
                // .ToListAsync();
                //var result2 = UserRepository.Where(x => x.Id == userId).SingleOrDefault();


                //var test = await ctx.Set<ApplicationUser>().Where(c => c.Id == userId)
                //    .Select(d => new
                //{
                //    user = d,
                //    groupsToManage = ctx.Set<ApplicationUserGroups>()
                //        .Where(x => x.UserId == userId)
                //        .Join(
                //            ctx.Set<Groups>(),
                //            a_u => a_u.GroupId,
                //            g => g.Id,
                //            (u, g) => g),
                //        gro2 = ctx.Set<MemberGroups>().Where(x => x.UserId == userId).Join(
                //        ctx.Set<Groups>(),
                //        a_u => a_u.GroupId,
                //        g => g.Id,
                //        (u, g) => g)
                //    }).FirstOrDefaultAsync();

                //var user = await ctx.Set<ApplicationUser>().FirstOrDefaultAsync(x => x.Id == userId);
                //var groups = await ctx.Set<Groups>().Where(x => x.MemberGroups.Where(g => g.UserId == userId)).ToListAsync(); 

                //var test = await ctx.Set<ApplicationUser>()
                //    .Include(c => c.ApplicationUserGroups).ThenInclude(d => d.Group)
                //    .Include(c => c.MemberGroups).ThenInclude(d => d.Group).FirstOrDefaultAsync(c => c.Id == userId);

                //var test = ctx.Set<ApplicationUser>()
                //    .Where(x => x.Id == userId)
                //    .SelectMany(t => t.ApplicationUserGroups.Select(x => x.Group), (usr, usrgroup) => new { usr, usrgroup })
                //    .SelectMany(g => g.usrgroup.MemberGroups.Select(x => x.Group), (usr, usrgroup) => new { usr, usrgroup })


                //   .ToList();

                //var test2 = ctx.Set<ApplicationUser>()
                //   .Where(x => x.Id == userId)
                //   .SelectMany(t => t.MemberGroups.Select(x => x.Group), (usr, usrgroup) => new { usr, usrgroup })
                //  .ToList()
                //  .GroupBy(x => x.usrgroup.Id)
                //  .Select(x => new { x.Key, MemberGroups = x.Select(z => z.usrgroup) })
                //  .ToList();




                //var test2 = await ctx.Set<ApplicationUser>()
                // //.Where(x => x.Id == userId)
                // .GroupJoin(ctx.ApplicationUserGroups.Include(x => x.Group), usr => usr.Id, aug => aug.UserId, (usr, augroups) => new { usr, groups = augroups.Select(x => x.Group) })
                // .GroupJoin(ctx.MemberGroups.Include(x => x.Group), usr => usr.usr.Id, mg => mg.UserId, (usr, mgroups) => new { usr, memgroups = mgroups.Select(x => x.Group) })
                // .Select(x => new { Id = x.usr.usr.Id, ApplicationUserGroups = x.usr.groups, MemberGroups = x.memgroups })
                // .FirstOrDefaultAsync(x => x.Id == userId);



                var test3 = UserRepository
                .GroupJoin(MemberGroupRepository
                 .Join(GroupRepository,
                     k => k.GroupId,
                     group => group.Id,
                     (k, group) => new { k, group }
                     ),
                       user => user.Id,
                       res1 => res1.k.UserId,
                       (user, res1) => new { user, group1 = res1.Select(x => x.group) })
                .GroupJoin(A_UGroupRepository
                 .Join(GroupRepository,
                     k => k.GroupId,
                     group => group.Id,
                     (k, group) => new { k, group }
                     ),
                      t => t.user.Id,
                       res2 => res2.k.UserId,
                       (t, res2) => new { userId = t.user.Id, data1 = t.group1, data2 = res2.Select(x => x.group.Id) })
                .SingleOrDefaultAsync(x => x.userId == userId).Result;



                //.GroupJoin(ctx.MemberGroups.Include(x => x.Group), usr => usr.usr.Id, mg => mg.UserId, (usr, mgroups) => new { usr, memgroups = mgroups.Select(x => x.Group) })
                //.Select(x => new { Id = x.usr.usr.Id, ApplicationUserGroups = x.usr.groups, MemberGroups = x.memgroups })
                //.FirstOrDefaultAsync(x => x.Id == userId);


                //result = 
                //{
                //    GroupsToManage = test.ApplicationUserGroups.Select(c => c.Group).ToList(),
                //    GroupsToMember = test.MemberGroups.Select(c => c.Group).ToList()
                //};

            }

            return result;
        }

    }


    class Item
    {
        public Guid MemId { get; set; }
        public Guid GroupId { get; set; }
        public string UserId { get; set; }
    }
}
