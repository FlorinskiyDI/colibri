using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using storagecore.Abstractions.Uow;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Entities;

namespace Survey.ApplicationLayer.Services
{


    public class ServeySectionRespondentServie : IServeySectionRespondentServie
    {


        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;

        public ServeySectionRespondentServie(
            IUowProvider uowProvider,
            IMapper mapper
        )
        {
            this.UowProvider = uowProvider;
            this.Mapper = mapper;
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
