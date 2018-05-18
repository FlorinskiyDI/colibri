using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using storagecore.Abstractions.Uow;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Entities;

namespace Survey.ApplicationLayer.Services
{

    public class InputTypeService : IInputTypeService
    {

        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;

        public InputTypeService(
            IUowProvider uowProvider,
            IMapper mapper
        )
        {
            this.UowProvider = uowProvider;
            this.Mapper = mapper;
        }

        public IEnumerable<InputTypesDto> GetAll()
        {
            IEnumerable<InputTypes> items;
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositoryInputType = uow.GetRepository<InputTypes, Guid>();
                items = repositoryInputType.GetAll();
            }
            return Mapper.Map<IEnumerable<InputTypes>, IEnumerable<InputTypesDto>>(items);
        }
    }
}
