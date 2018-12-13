using System;
using System.Threading.Tasks;
using AutoMapper;
using dataaccesscore.Abstractions.Uow;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Entities;

namespace Survey.ApplicationLayer.Services
{

    public class OptionGroupService : IOptionGroupService
    {
        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;

        public OptionGroupService(
            IUowProvider uowProvider,
            IMapper mapper
        )
        {
            this.UowProvider = uowProvider;
            this.Mapper = mapper;
        }

        public async Task<Guid> AddAsync(string groupDefinition)
        {
            OptionGroupsDto optiongroup = new OptionGroupsDto()
            {
                Name = groupDefinition
            };
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                OptionGroups optiongroupEntity = Mapper.Map<OptionGroupsDto, OptionGroups>(optiongroup);
                var repositoryPage = uow.GetRepository<OptionGroups, Guid>();
                await repositoryPage.AddAsync(optiongroupEntity);
                await uow.SaveChangesAsync();

                return optiongroupEntity.Id;
            }
        }
    }
}
