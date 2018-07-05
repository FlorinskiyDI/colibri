using Survey.ApplicationLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Survey.ApplicationLayer.Dtos.Models.Answers;
using storagecore.Abstractions.Uow;
using AutoMapper;
using System.Threading.Tasks;
using Survey.DomainModelLayer.Entities;

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




        public Guid Add(BaseAnswerModel item, Guid optionChoiceId)
        {
            QuestionOptions questionOption = new QuestionOptions()
            {
                QuestionId = item.Id,
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
