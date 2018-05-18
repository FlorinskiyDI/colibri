using AutoMapper;
using storagecore.Abstractions.Uow;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Survey.DomainModelLayer.Entities;

namespace Survey.ApplicationLayer.Services
{
    public class PageService : IPageService
    {

        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;

        public PageService(
            IUowProvider uowProvider,
            IMapper mapper
        )
        {
            this.UowProvider = uowProvider;
            this.Mapper = mapper;
        }


        public async Task<Guid> AddAsync(PageModel survey, Guid surveyId)
        {
            string surveystring = surveyId.ToString();
            PagesDto pageDto = new PagesDto()
            {
                Description = survey.Description,
                Name = survey.Name,
                OrderNo = survey.OrderNo,
                SurveyId = Guid.Parse(surveyId.ToString()) 
            };

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                try
                {
                    Pages pageEntity = Mapper.Map<PagesDto, Pages>(pageDto);
                    var repositoryPage = uow.GetRepository<Pages, Guid>();
                    await repositoryPage.AddAsync(pageEntity);
                    await uow.SaveChangesAsync();

                    return pageEntity.Id;
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
