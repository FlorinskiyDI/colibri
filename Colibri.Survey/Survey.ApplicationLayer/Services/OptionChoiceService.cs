using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using storagecore.Abstractions.Uow;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Entities;

namespace Survey.ApplicationLayer.Services
{
    public class OptionChoiceService : IOptionChoiceService
    {

        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;

        public OptionChoiceService(
            IUowProvider uowProvider,
            IMapper mapper
        )
        {
            this.UowProvider = uowProvider;
            this.Mapper = mapper;
        }

        public async void AddAsync(Guid optionGroupId, ItemModel item = null)
        {
            OptionChoisesDto optionChoisesDto = new OptionChoisesDto()
            {
                 Name = item != null ? item.Value : "",
                 OptionGroupId = optionGroupId,
                 OrderNo = 1 // stub
            };

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                try
                {
                    OptionChoises optionChoisesEntity = Mapper.Map<OptionChoisesDto, OptionChoises>(optionChoisesDto);
                    var repositoryOptionChoise = uow.GetRepository<OptionChoises, Guid>();
                    await repositoryOptionChoise.AddAsync(optionChoisesEntity);
                    await uow.SaveChangesAsync();
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
