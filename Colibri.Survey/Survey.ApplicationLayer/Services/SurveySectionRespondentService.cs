using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dataaccesscore.Abstractions.Uow;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Entities;

namespace Survey.ApplicationLayer.Services
{


    public class SurveySectionRespondentServie : ISurveySectionRespondentService
    {


        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;

        public SurveySectionRespondentServie(
            IUowProvider uowProvider,
            IMapper mapper
        )
        {
            this.UowProvider = uowProvider;
            this.Mapper = mapper;
        }


        public async Task<int> GetListBySurveyId(Guid id)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositorySR = uow.GetRepository<SurveySectoinRespondents, Guid>();
                var items = await repositorySR.QueryAsync(x => x.SurveySectionId == id);
                return items.Count();
            }
        }




        public async Task<Guid> AddAsync(Guid surveyId, Guid RespondentId)
        {

            SurveySectoinRespondents item = new SurveySectoinRespondents
            {
                RespondentId = RespondentId,
                SurveySectionId = surveyId
            };


            using (var uow = UowProvider.CreateUnitOfWork())
            {
                try
                {

                    var repositorySR = uow.GetRepository<SurveySectoinRespondents, Guid>();
                    await repositorySR.AddAsync(item);
                    await uow.SaveChangesAsync();

                    return item.Id;
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
