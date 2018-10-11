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



        public async Task<IEnumerable<OptionChoises>> GetListByOptionGroupId(Guid? optionGroupId)
        {
            try
            {


                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repositoryOptionChoice = uow.GetRepository<OptionChoises, Guid>();
                    var choises = await repositoryOptionChoice.QueryAsync(item => item.OptionGroupId == optionGroupId);
                    await uow.SaveChangesAsync();
                    return choises;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void UpdateOptionChoise(OptionChoises choise)
        {
            try
            {
                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repositoryChoice = uow.GetRepository<OptionChoises, Guid>();
                    repositoryChoice.Update(choise);
                    uow.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void DeleteOptionChoise(OptionChoises choise)
        {
            try
            {
                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repositoryChoice = uow.GetRepository<OptionChoises, Guid>();
                    repositoryChoice.Remove(choise);
                    uow.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<List<ItemModel>> GetListByOptionGroup(Guid? optionGroupId)
        {
            try
            {
                List<ItemModel> items = new List<ItemModel>();
                IEnumerable<OptionChoises> choises;
                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repositoryOptionChoice = uow.GetRepository<OptionChoises, Guid>();
                    choises = await repositoryOptionChoice.QueryAsync(item => item.OptionGroupId == optionGroupId);
                    await uow.SaveChangesAsync();
                    IEnumerable<OptionChoisesDto> optionChoisesDto = Mapper.Map<IEnumerable<OptionChoises>, IEnumerable<OptionChoisesDto>>(choises);

                    foreach (var item in optionChoisesDto)
                    {
                        ItemModel page = new ItemModel()
                        {
                            Id = item.Id.ToString(),
                            Value = item.Name,
                            Order = 0, // strub
                            Label = ""

                        };
                        items.Add(page);
                    }
                    return items;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<Guid> AddAsync(Guid optionGroupId, ItemModel item = null)
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
                    return optionChoisesDto.Id;

                }
                catch (Exception e)
                {
                    Console.Write(e);
                    throw;
                }
            }
        }



        public void AddRange(Guid optionGroupId, List<ItemModel> items)
        {
            List<OptionChoisesDto> listchoiseDto = new List<OptionChoisesDto>();

            foreach (var item in items)
            {
                OptionChoisesDto optionChoisesDto = new OptionChoisesDto()
                {
                    Name = item != null ? item.Value : "",
                    OptionGroupId = optionGroupId,
                    OrderNo = 1 // stub
                };
                listchoiseDto.Add(optionChoisesDto);
            }


            using (var uow = UowProvider.CreateUnitOfWork())
            {
                try
                {
                    List<OptionChoises> optionChoisesEntity = Mapper.Map<List<OptionChoisesDto>, List<OptionChoises>>(listchoiseDto);
                    var repositoryOptionChoise = uow.GetRepository<OptionChoises, Guid>();

                    //var list = Mapper.Map<IEnumerable<Groups>, IEnumerable<GroupDto>>(result);
                    repositoryOptionChoise.AddRange(optionChoisesEntity.ToArray());
                    uow.SaveChanges();

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
