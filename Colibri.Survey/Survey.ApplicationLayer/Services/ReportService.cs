using AutoMapper;
using Survey.ApplicationLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Survey.ApplicationLayer.Dtos.Models.Report;
using Survey.DomainModelLayer.Entities;
using Survey.Common.Enums;
using dataaccesscore.Abstractions.Uow;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services
{
    class ReportService : IReportService
    {
        QuestionTypes type;
        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;
        protected readonly IOptionChoiceService optionChoiceService;

        private List<TableReportViewModel> _rowQuestions;

        public ReportService(
            IUowProvider uowProvider,
            IOptionChoiceService optionChoiceService,
            IMapper mapper
        )
        {
            this.optionChoiceService = optionChoiceService;
            this.UowProvider = uowProvider;
            this.Mapper = mapper;

            _rowQuestions = new List<TableReportViewModel>();
        }

        public List<ColumModel> GetQuestions(Guid surveyId)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositorySurvey = uow.GetRepository<SurveySections, Guid>();
                var repositoryPage = uow.GetRepository<Pages, Guid>();
                var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                var repositoryInputType = uow.GetRepository<InputTypes, Guid>();


                try
                {
                    var questionList = repositorySurvey.Query(p => p.Id == surveyId)
                  .Join(repositoryPage.GetAll(),
                      survey => survey.Id,
                      pages => pages.SurveyId,
                      (survey, page) => new { survey, page })

                  .Join(repositoryQuestion.GetAll(),
                      surveyPageEntry => surveyPageEntry.page.Id,
                      questions => questions.PageId,
                      (surveyPageEntry, question) => new { surveyPageEntry, question })

                  .Join(repositoryInputType.GetAll(),
                      typeNeatCombeEntry => typeNeatCombeEntry.question.InputTypesId,
                      inputType => inputType.Id,
                      (nestedComboEntity, inputType) => new ColumModel()
                      {
                          Id = nestedComboEntity.question.Id,
                          Name = nestedComboEntity.question.Name,
                          Type = inputType.Name,
                          OrderNo = nestedComboEntity.question.OrderNo,
                          ParentId = nestedComboEntity.question.ParentId,
                          PageOrderNo = nestedComboEntity.surveyPageEntry.page.OrderNo,
                          AdditionalAnswer = IsExcistAdditionalOptionChoice(nestedComboEntity.question.OptionGroupId).Result
                      })
                  .OrderBy(p => p.PageOrderNo).ThenBy(p => p.OrderNo).ToList();





                    //var result = questionList
                    //    .GroupBy(u => u.ParentId == null)
                    //    .Select(grp => grp.ToList()).ToList();


                    var result = questionList
                        .GroupBy(u => u.ParentId == null).ToList();

                    List<ColumModel> group = new List<ColumModel>();
                    if (result.Count() > 0)
                    {
                        group = result.Single(g => g.Key == true).ToList();
                        //var res = result.FindAll().Key(true);
                        foreach (var question in group)
                        {
                            if (question.Type == QuestionTypes.GridRadio.ToString())
                            {
                                question.Children = result.Single(g => g.Key == false).Where(p => p.ParentId == question.Id).Select(c => c.Name).ToList<string>();
                            }
                        }
                    }
                    return group;

                }
                catch (Exception ex)
                {
                    var catc = ex;
                    throw;
                }

            }

        }


        public async Task<bool> IsExcistAdditionalOptionChoice(Guid? optionGroupId)
        {
            var optionChoices = await optionChoiceService.GetListByOptionGroupId(optionGroupId, true);
            var item = optionChoices.Where(x => x.IsAdditionalChoise == true).SingleOrDefault();
            if (item != null)
            {
                return true;
            }
            return false;
        }




        public List<TableReportViewModel> GetQuesionListBySurveyId(Guid surveyId)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositoryRespondent = uow.GetRepository<SurveySectoinRespondents, Guid>();
                var repositorySurvey = uow.GetRepository<SurveySections, Guid>();
                var repositoryPage = uow.GetRepository<Pages, Guid>();
                var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                var repositoryInputType = uow.GetRepository<InputTypes, Guid>();

                var answerList = repositoryRespondent.GetAll()


                    .Join(repositorySurvey.Query(p => p.Id == surveyId),
                        surveyResp => surveyResp.SurveySectionId,
                        survey => survey.Id,
                        (survResp, surv) => new { survResp, surv })


                    .Join(repositoryPage.GetAll(),
                        combinedEntry => combinedEntry.surv.Id,
                        pages => pages.SurveyId,
                        (combeEntry, page) => new { combeEntry, page })


                    .Join(repositoryQuestion.GetAll(),
                        seedCombinedEntry => seedCombinedEntry.page.Id,
                        questions => questions.PageId,
                        (neatCombeEntry, question) => new { neatCombeEntry, question })


                    .Join(repositoryInputType.GetAll(),
                        typeNeatCombeEntry => typeNeatCombeEntry.question.InputTypesId,
                        inputType => inputType.Id,
                        (typeNeatCombeEntry, inputType) => new TableReportViewModel()
                        {
                            GroupId = typeNeatCombeEntry.question.OptionGroupId,
                            InputTypeName = inputType.Name,
                            QuestionId = typeNeatCombeEntry.question.Id,
                            ParentQuestionId = typeNeatCombeEntry.question.ParentId,
                            QuestionName = typeNeatCombeEntry.question.Name,
                            RespondentId = typeNeatCombeEntry.neatCombeEntry.combeEntry.survResp.RespondentId,
                            OrderNo = typeNeatCombeEntry.question.OrderNo,
                            PageOrderNo = typeNeatCombeEntry.neatCombeEntry.page.OrderNo
                        })
                    .OrderBy(p => p.PageOrderNo).ThenBy(p => p.OrderNo).ToList();





                if (answerList.Count > 0)
                {
                    var groupes = answerList.GroupBy(u => u.ParentQuestionId == null).ToArray();
                    //.GroupBy(u => u.ParentQuestionId == null)
                    var GroupAnswer = groupes.Single(g => g.Key == true).ToList();



                    var groupedCustomerList = answerList
                        .GroupBy(u => u.ParentQuestionId == null)
                        .Select(grp => grp.ToList())
                        .ToList();

                    if (groupedCustomerList.Count > 1)
                    {
                        _rowQuestions = groupes.Single(g => g.Key == false).ToList(); ;
                    }
                    return GetReport(GroupAnswer);
                }
                else
                {
                    return answerList;
                }

            }
        }

        public List<TableReportViewModel> GetReport(List<TableReportViewModel> answersData)
        {

            try
            {
                if (answersData.Count > 0)
                {
                    using (var uow = UowProvider.CreateUnitOfWork())
                    {
                        var repositoryRespondent = uow.GetRepository<Respondents, Guid>();
                        var repositoryAnswer = uow.GetRepository<Answers, Guid>();
                        var repositoryQuestion_Options = uow.GetRepository<QuestionOptions, Guid>();
                        var repositoryOptionChoice = uow.GetRepository<OptionChoises, Guid>();
                        var repositoryQuestion = uow.GetRepository<Questions, Guid>();

                        foreach (var item in answersData)
                        {
                            List<AnswerModel> answer = new List<AnswerModel>();
                            answer = repositoryRespondent.Query(p => p.Id == item.RespondentId)
                                .Join(repositoryAnswer.GetAll(),
                                    surveyResp => surveyResp.Id,
                                    answers => answers.RespondentId,
                                    (survResp, answers) => new { survResp, answers })
                                .Join(repositoryQuestion_Options.Query(p => p.QuestionId == item.QuestionId),
                                    combinedEntry => combinedEntry.answers.QuestionOptionId,
                                    question_option => question_option.Id,
                                    (combeEntry, ques_opt) => new { combeEntry, ques_opt })

                                .Join(repositoryOptionChoice.GetAll(),
                                    oCCombo => oCCombo.ques_opt.OptionChoiseId,
                                    optionChoice => optionChoice.Id,
                                    (oCCombo, optionChoice) => new { oCCombo, optionChoice }
                                )
                                .Join(repositoryQuestion.GetAll(),
                                    combo => combo.oCCombo.ques_opt.QuestionId,
                                    question => question.Id,
                                    (combo, question) => new AnswerModel()
                                    {
                                        AnswerText = combo.oCCombo.combeEntry.answers.AnswerText,
                                        IsAdditional = combo.optionChoice.IsAdditionalChoise,
                                        OptionChoise = combo.optionChoice.Name
                                    }
                                )
                                .ToList();
                            item.Answer = GetAnswerByType(item, answer, item.QuestionId, item.RespondentId);

                            var check = IsExcistAdditionalOptionChoice(item.GroupId).Result;
                            //item.AdditionalAnswer = IsExcistAdditionalOptionChoice(item.GroupId).Result ? "NULL" : "";
                            item.AdditionalAnswer = item.AdditionalAnswer.Count() > 0 ? item.AdditionalAnswer : (IsExcistAdditionalOptionChoice(item.GroupId).Result ? "--NOT SET--" : "");
                        }
                    }
                }
                var result = answersData
                    .GroupBy(u => u.RespondentId)
                    .Select(grp => grp.ToList())
                    .ToList();
                return answersData;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        protected object GetAnswerByType(TableReportViewModel answerModel, List<AnswerModel> answerList, Guid questionId, Guid respondentId)
        {
            string answerONQuestion = "--NULL--";
            if (Enum.TryParse(answerModel.InputTypeName, out type))
            {
                switch (type)
                {
                    case QuestionTypes.Textbox:
                        {
                            if (answerList.Count > 0)
                            {
                                answerONQuestion = answerList.FirstOrDefault().AnswerText;
                            }
                            break;
                        }
                    case QuestionTypes.Textarea:
                        {
                            if (answerList.Count > 0)
                            {
                                answerONQuestion = answerList.FirstOrDefault().AnswerText;
                            }
                            break;
                        }
                    case QuestionTypes.Radio:
                        {
                            foreach (var item in answerList)
                            {
                                if (item.IsAdditional)
                                {
                                    answerModel.AdditionalAnswer = item.AnswerText;
                                }
                                else
                                {
                                    answerONQuestion = item.OptionChoise;
                                }
                            }
                            break;
                        }
                    case QuestionTypes.Checkbox:
                        {
                            if (answerList.Count > 0)
                            {
                                answerONQuestion = "";
                                foreach (var item in answerList)
                                {
                                    if (item.IsAdditional)
                                    {
                                        answerModel.AdditionalAnswer = item.AnswerText;
                                    }
                                    else
                                    {
                                        answerONQuestion = answerONQuestion + "," + item.OptionChoise;
                                    }

                                }
                            }
                            break;
                        }
                    case QuestionTypes.Dropdown:
                        {
                            foreach (var item in answerList)
                            {
                                if (item.IsAdditional)
                                {
                                    answerModel.AdditionalAnswer = item.AnswerText;
                                }
                                else
                                {
                                    answerONQuestion = item.OptionChoise;
                                }
                            }
                            break;
                        }
                    case QuestionTypes.GridRadio:
                        {
                            if (answerList.Count > 0)
                            {
                                answerModel.AdditionalAnswer = answerList.SingleOrDefault().AnswerText;
                            }

                            //if (item.IsAdditional)
                            //{
                            //    answerModel.AdditionalAnswer = item.AnswerText;
                            //}
                            List<TableReportViewModel> tempList = new List<TableReportViewModel>();
                            var rowQuestionList = _rowQuestions
                                .Where(p => p.ParentQuestionId == questionId).Where(p => p.RespondentId == respondentId).ToList();

                            //tempList.Add(rowQuestionList);
                            var newanser = GetReport(rowQuestionList);
                            return newanser;
                            //break;
                        }
                }
            }
            else
            {
                var check = 5;
            }
            return answerONQuestion;
        }
    }
}
