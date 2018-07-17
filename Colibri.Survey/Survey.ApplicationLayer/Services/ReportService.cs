using AutoMapper;
using storagecore.Abstractions.Uow;
using Survey.ApplicationLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Survey.ApplicationLayer.Dtos.Models.Report;
using Survey.DomainModelLayer.Entities;
using Newtonsoft.Json;
using Survey.Common.Enums;

namespace Survey.ApplicationLayer.Services
{
    class ReportService : IReportService
    {
        QuestionTypes type;
        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;

        public ReportService(
            IUowProvider uowProvider,
            IMapper mapper
        )
        {
            this.UowProvider = uowProvider;
            this.Mapper = mapper;
        }

        public List<ColumModel> GetQuestions(Guid surveyId)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositorySurvey = uow.GetRepository<SurveySections, Guid>();
                var repositoryPage = uow.GetRepository<Pages, Guid>();
                var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                var repositoryInputType = uow.GetRepository<InputTypes, Guid>();

                var questionList = repositorySurvey.GetAll()
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
                            ParentId = nestedComboEntity.question.ParentId
                        })
                    .OrderBy(p => p.OrderNo).GroupBy(u => u.ParentId == null)
                    .Select(grp => grp.ToList())
                    .ToList();


                foreach (var question in questionList[0]) { 
                    if (question.Type == QuestionTypes.GridRadio.ToString())
                    {
                        question.Children = questionList[1].Where(p => p.ParentId == question.Id).Select(c =>c.Name).ToList<string>();
                    }
                }



                return questionList[0];

            }
       
        }


        public List<List<TableReportViewModel>> GetQuesionListBySurveyId(Guid surveyId)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var id = new Guid();
                var repositoryRespondent = uow.GetRepository<SurveySectoinRespondents, Guid>();
                var repositorySurvey = uow.GetRepository<SurveySections, Guid>();
                var repositoryPage = uow.GetRepository<Pages, Guid>();
                var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                var repositoryInputType = uow.GetRepository<InputTypes, Guid>();

                var questionList = repositoryRespondent.GetAll()


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
                            InputTypeName = inputType.Name,
                            PageId = typeNeatCombeEntry.neatCombeEntry.page.Id,
                            QuestionId = typeNeatCombeEntry.question.Id,
                            ParentQuestionId = typeNeatCombeEntry.question.ParentId,
                            QuestionName = typeNeatCombeEntry.question.Name,
                            RespondentId = typeNeatCombeEntry.neatCombeEntry.combeEntry.survResp.RespondentId
                        })
                        .OrderBy(p => p.RespondentId).ToList();
                //uow.SaveChanges();


                //var grop = questionList.GroupBy(p => p.ParentQuestionId != null).ToList();


                var groupedCustomerList = questionList
                    .GroupBy(u => u.ParentQuestionId == null)
                    .Select(grp => grp.ToList())
                    .ToList();




                //var indices = questionList
                //    .Where(x => x.ParentQuestionId.ToString().Length > 0)
                //    .Select(x => questionList.IndexOf(x))
                //    .ToList();

                //var groups = indices
                //    .Select((x, idx) => idx != indices.Count - 1
                //        ? questionList.GetRange(x, indices[idx + 1] - x)
                //        : questionList.GetRange(x, questionList.Count - x))
                //    .ToList();


                return groupedCustomerList;
            }
        }

        public List<TableReportViewModel> GetReport(List<List<TableReportViewModel>> answersData)
        {
            try
            {
                if (answersData[0].Count > 0)
                {
                    using (var uow = UowProvider.CreateUnitOfWork())
                    {
                        var repositoryRespondent = uow.GetRepository<Respondents, Guid>();
                        var repositoryAnswer = uow.GetRepository<Answers, Guid>();
                        var repositoryQuestion_Options = uow.GetRepository<QuestionOptions, Guid>();
                        var repositoryOptionChoice = uow.GetRepository<OptionChoises, Guid>();
                        var repositoryQuestion = uow.GetRepository<Questions, Guid>();


                        foreach (var item in answersData[0])
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
                                        OptionChoise = combo.optionChoice.Name
                                    }
                                )
                                .ToList();

                            
                                item.Answer = getAnswerByType(item, answer);





                            if (item.ParentQuestionId != null )
                            {
                                item.Answer = getAnswerByType(item, answer);
                            }
                            else // if question is grid type
                            {
                                List<List<TableReportViewModel>> list = new List<List<TableReportViewModel>>();
                                var rowQuestionList = answersData[1]
                                    .Where(p => p.ParentQuestionId == item.QuestionId).ToList();

                                list.Add(rowQuestionList);
                                var newanser = GetReport(list).Where(p => p.RespondentId == item.RespondentId).ToList();
                                item.Answer = newanser;
                            }
                        }
                    }
                }
                var result = answersData[0]
                    .GroupBy(u => u.RespondentId)
                    .Select(grp => grp.ToList())
                    .ToList();
                return answersData[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }


        protected string getAnswerByType(TableReportViewModel answerModel, List<AnswerModel> answerList)
        {
            string answerONQuestion = "NOT ANSWER";
            //BaseQuestionModel baseQuestionM = JsonConvert.DeserializeObject<BaseQuestionModel>(baseQuestion);
            if (Enum.TryParse(answerModel.InputTypeName, out type) && answerList.Count > 0)
            {
                switch (type)
                {
                    case QuestionTypes.Textbox:
                        {
                            if (answerList.FirstOrDefault().AnswerText.Length > 0)
                            {
                                answerONQuestion = answerList.FirstOrDefault().AnswerText;
                            }
                            break;
                        }
                    case QuestionTypes.Textarea:
                        {
                            if (answerList.FirstOrDefault().AnswerText.Length > 0)
                            {
                                answerONQuestion = answerList.FirstOrDefault().AnswerText;
                            }
                            break;
                        }
                    //case QuestionTypes.Radio:
                    //    {
                    //        RadioQuestionModel question = JsonConvert.DeserializeObject<RadioQuestionModel>(baseQuestion);
                    //        baseQuestionM = question as BaseQuestionModel;
                    //        break;
                    //    }
                    case QuestionTypes.Checkbox:
                        {
                            if (answerList.Count > 0)
                            {
                                answerONQuestion = "";
                                foreach (var item in answerList)
                                {
                                    answerONQuestion = answerONQuestion + "," + item.OptionChoise;
                                }
                            }
                            break;
                        }
                    //case QuestionTypes.Dropdown:
                    //    {
                    //        DropdownQuestionModel question = JsonConvert.DeserializeObject<DropdownQuestionModel>(baseQuestion);
                    //        baseQuestionM = question as BaseQuestionModel;
                    //        break;
                    //    }
                    case QuestionTypes.GridRadio:
                        {
                            if (answerList.Count > 0)
                            {
                                answerONQuestion = "";
                                foreach (var item in answerList)
                                {
                                    answerONQuestion = answerONQuestion + "," + item.OptionChoise;
                                }
                            }
                            break;
                        }
                }
            }



            return answerONQuestion;
        }
    }
}
