using AutoMapper;
using storagecore.Abstractions.Uow;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services
{
    public class UserService : IUserService
    {
        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;

        public UserService(
            IUowProvider uowProvider,
            IMapper mapper
        )
        {
            this.UowProvider = uowProvider;
            this.Mapper = mapper;
        }

        public async Task<Guid> AddAsync(Guid identityUserId)
        {

            UsersDto userDto = new UsersDto()
            {
                Id = identityUserId
            };


            using (var uow = UowProvider.CreateUnitOfWork())
            {
                try
                {
                    Users userEntity = Mapper.Map<UsersDto, Users>(userDto);
                    var repositoryUser = uow.GetRepository<Users, Guid>();
                    await repositoryUser.AddAsync(userEntity);
                    await uow.SaveChangesAsync();

                    return userEntity.Id;
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    throw;
                }
            }

        }


        public async Task<UsersDto> GetAsync(Guid userId)
        {
            try
            {
                Users item;
                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repositoryUser = uow.GetRepository<Users, Guid>();
                    item = await repositoryUser.GetAsync(userId);

                    return Mapper.Map<Users, UsersDto>(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
