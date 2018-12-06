using Survey.ApplicationLayer.Services.Interfaces;
using System;
using AutoMapper;
using Survey.DomainModelLayer.Entities;
using dataaccesscore.Abstractions.Uow;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Survey.ApplicationLayer.Services
{
    public class QuestionOptionService : IQuestionOptionService
    {
        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;


        public QuestionOptionService(
            IUowProvider uowProvider,
            IMapper mapper

        )
        {
            this.UowProvider = uowProvider;
            this.Mapper = mapper;

        }




        public Guid Add(Guid questoinId, Guid optionChoiceId)
        {
            QuestionOptions questionOption = new QuestionOptions()
            {
                QuestionId = questoinId,
                OptionChoiseId = optionChoiceId
            };

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                try
                {
                    //Questions questionEntity = Mapper.Map<QuestionsDto, Questions>(questionDto);
                    var repositoryQuestionOption = uow.GetRepository<QuestionOptions, Guid>();
                    repositoryQuestionOption.Add(questionOption);
                    uow.SaveChanges();

                    return questionOption.Id;
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    throw;
                }
            }

        }


        public async Task Remove(QuestionOptions q_o)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositoryQuestionOption = uow.GetRepository<QuestionOptions, Guid>();

                repositoryQuestionOption.Remove(q_o);
                uow.SaveChanges();
            }
        }

        public async Task<IEnumerable<QuestionOptions>> GetAllAsync()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositoryQuestionOption = uow.GetRepository<QuestionOptions, Guid>();

                var result = await repositoryQuestionOption.GetAllAsync();
                return result;
            }
        }

    }



    //public async Task<Guid> AddAsync(PageModel survey, Guid surveyId)
    //{
    //string surveystring = surveyId.ToString();
    //PagesDto pageDto = new PagesDto()
    //    {
    //        Description = survey.Description,
    //        Name = survey.Name,
    //        OrderNo = survey.OrderNo,
    //        SurveyId = Guid.Parse(surveyId.ToString())
    //    };

    //    using (var uow = UowProvider.CreateUnitOfWork())
    //{
    //    try
    //    {
    //        Pages pageEntity = Mapper.Map<PagesDto, Pages>(pageDto);
    //        var repositoryPage = uow.GetRepository<Pages, Guid>();
    //        await repositoryPage.AddAsync(pageEntity);
    //        await uow.SaveChangesAsync();

    //        return pageEntity.Id;
    //    }
    //    catch (Exception e)
    //    {
    //        Console.Write(e);
    //        throw;
    //    }
    //}

}
