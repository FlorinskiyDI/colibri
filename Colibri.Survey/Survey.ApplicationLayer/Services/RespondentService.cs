using System;
using System.Threading.Tasks;
using AutoMapper;
using dataaccesscore.Abstractions.Uow;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Entities;

namespace Survey.ApplicationLayer.Services
{
    public class RespondentService : IRespondentService
    {


        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;

        public RespondentService(
            IUowProvider uowProvider,
            IMapper mapper
        )
        {
            this.UowProvider = uowProvider;
            this.Mapper = mapper;
        }



        public async Task<Guid> AddAsync()
        {

            Respondents respondent = new Respondents();


            using (var uow = UowProvider.CreateUnitOfWork())
            {
                try
                {

                    var repositoryRspondent = uow.GetRepository<Respondents, Guid>();
                    await repositoryRspondent.AddAsync(respondent);
                    await uow.SaveChangesAsync();

                    return respondent.Id;
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
