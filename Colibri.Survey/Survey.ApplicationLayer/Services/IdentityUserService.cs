using AutoMapper;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Models;
using Survey.InfrastructureLayer.IdentityServerServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services
{
    public class IdentityUserService: IIdentityUserService
    {
        private readonly IIdentityUserRequestService _identityUserRequestService;
        protected readonly IMapper Mapper;
        public IdentityUserService(
            IIdentityUserRequestService identityUserRequestService,
            IMapper mapper
        )
        {
            _identityUserRequestService = identityUserRequestService;
            Mapper = mapper;
        }

        public async Task<IEnumerable<IdentityUserDto>> GetIdentityUsersForGroupAsync(Guid groupId)
        {
            var result = await _identityUserRequestService.GetIdentityUsersAsync(groupId);
            var list = Mapper.Map<IEnumerable<IdentityUserModel>, IEnumerable<IdentityUserDto>>(result);
            return list;
        }
    }
}
