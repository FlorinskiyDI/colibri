using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using storagecore.Abstractions.Uow;
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

        public async Task<Guid> AddAsync()
        {
            OptionGroupsDto optiongroup = new OptionGroupsDto()
            {
                Name = "textbox group"
            };
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                try
                {
                    OptionGroups optiongroupEntity = Mapper.Map<OptionGroupsDto, OptionGroups>(optiongroup);
                    var repositoryPage = uow.GetRepository<OptionGroups, Guid>();
                    await repositoryPage.AddAsync(optiongroupEntity);
                    await uow.SaveChangesAsync();

                    return optiongroupEntity.Id;
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    throw;
                }
            }
        }


    }
}
