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
                var repositoryPage = uow.GetRepository<Pages, Guid>();
                var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                var repositoryInputType = uow.GetRepository<InputTypes, Guid>();

                var questions = repositoryPage.Query(x => x.SurveyId == surveyId).AsQueryable()
                .Join(repositoryQuestion.GetAll().AsQueryable(),
                    page => page.Id,
                    question => question.PageId,
                    (page, question) => new { page, question })
                .Join(repositoryInputType.GetAll().AsQueryable(),
                    p_q => p_q.question.InputTypesId,
                    inputType => inputType.Id,
                    (p_q, inputType) => new ColumModel()
                    {
                        Id = p_q.question.Id,
                        Name = p_q.question.Name,
                        Type = inputType.Name,
                        OrderNo = p_q.question.OrderNo,
                        ParentId = p_q.question.ParentId,
                        PageOrderNo = p_q.page.OrderNo,
                        AdditionalAnswer = IsHaveAdditionalVariable(p_q.question.OptionGroupId).Result
                    })
                .OrderBy(p => p.PageOrderNo).ThenBy(p => p.OrderNo)
                .GroupBy(u => u.ParentId == null).ToList();

                List<ColumModel> clearQuestions = new List<ColumModel>();
                List<ColumModel> rowQuestions = new List<ColumModel>();
                if (questions.Count() > 0)
                {
                    clearQuestions = questions.Single(g => g.Key == true).ToList();
                    rowQuestions = questions.Count() > 1 ? questions.Single(g => g.Key == false).ToList() : rowQuestions;
                    //rowQuestions = questions.Single(g => g.Key == false).ToList();
                    foreach (var item in rowQuestions)
                    {
                        var clearQuestoin = clearQuestions.Where(x => x.Id == item.ParentId).SingleOrDefault();
                        clearQuestoin.Children.Add(item.Name);
                    }
                }
                return clearQuestions;
            }
        }


        public async Task<bool> IsHaveAdditionalVariable(Guid? optionGroupId)
        {
            var optionChoices = await optionChoiceService.GetListByOptionGroupId(optionGroupId, true);
            var item = optionChoices.Where(x => x.IsAdditionalChoise == true).SingleOrDefault();
            return item == null ? false : true;
        }


        public List<TableReportViewModel> GetAnswersBySurveyId(Guid id)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositoryS_R = uow.GetRepository<SurveySectoinRespondents, Guid>();
                var repositoryRespondent = uow.GetRepository<Respondents, Guid>();
                var repositoryPage = uow.GetRepository<Pages, Guid>();
                var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                var repositoryInputType = uow.GetRepository<InputTypes, Guid>();

                var answers = repositoryS_R.Query(x => x.SurveySectionId == id).AsQueryable()

                .Join(repositoryRespondent.GetAll().AsQueryable(),
                    d => d.RespondentId,
                    respondent => respondent.Id,
                    (SS_Respondent, respondent) => new { SS_Respondent, respondent })

                .Join(repositoryPage.GetAll().AsQueryable(),
                    x => x.SS_Respondent.SurveySectionId,
                    page => page.SurveyId,
                    (x, page) => new { x, page })

                .Join(repositoryQuestion.GetAll().AsQueryable(),
                    y => y.page.Id,
                    question => question.PageId,
                    (y, question) => new { y, question })

                .Join(repositoryInputType.GetAll().AsQueryable(),
                    b => b.question.InputTypesId,
                    inputType => inputType.Id,
                    (b, inputType) => new TableReportViewModel()
                    {
                        GroupId = b.question.OptionGroupId,
                        DateCreated = b.y.x.respondent.DateCreated,
                        InputTypeName = inputType.Name,
                        QuestionId = b.question.Id,
                        ParentQuestionId = b.question.ParentId,
                        QuestionName = b.question.Name,
                        RespondentId = b.y.x.respondent.Id,
                        OrderNo = b.question.OrderNo,
                        PageOrderNo = b.y.page.OrderNo
                    })
                .OrderBy(p => p.PageOrderNo).ThenBy(p => p.OrderNo).ToList();

                if (answers.Count > 0)
                {
                    var answerGroups = answers.GroupBy(u => u.ParentQuestionId == null).ToList();
                    var clearAnswer = answerGroups.Single(g => g.Key == true).ToList();

                    if (answerGroups.Count > 1)
                    {
                        _rowQuestions = answerGroups.Single(g => g.Key == false).ToList(); ;
                    }
                    return AddAnswerToReport(clearAnswer);
                }
                return answers;
            }
        }


        public List<TableReportViewModel> AddAnswerToReport(List<TableReportViewModel> answersData)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositoryAnswer = uow.GetRepository<Answers, Guid>();
                var repositoryQuestion_Options = uow.GetRepository<QuestionOptions, Guid>();
                var repositoryOptionChoice = uow.GetRepository<OptionChoises, Guid>();

                foreach (var item in answersData)
                {
                    List<AnswerModel> answerData = new List<AnswerModel>();
                    answerData = repositoryAnswer.Query(x => x.RespondentId == item.RespondentId).AsQueryable()

                        .Join(repositoryQuestion_Options.Query(p => p.QuestionId == item.QuestionId).AsQueryable(),
                            answer => answer.QuestionOptionId,
                            question_option => question_option.Id,
                            (answer, question_option) => new { answer, question_option })

                        .Join(repositoryOptionChoice.GetAll().AsQueryable(),
                            x => x.question_option.OptionChoiseId,
                            optionChoice => optionChoice.Id,
                            (x, optionChoice) => new AnswerModel()
                            {
                                AnswerText = x.answer.AnswerText,
                                AnswerBoolean = x.answer.AnswerBoolean,
                                IsAdditional = optionChoice.IsAdditionalChoise,
                                OptionChoise = optionChoice.Name
                            }
                        ).ToList();

                    item.Answer = GetAnswerByType(item, answerData);
                    item.AdditionalAnswer = item.AdditionalAnswer.Count() > 0 ? item.AdditionalAnswer : (IsHaveAdditionalVariable(item.GroupId).Result ? "--NOT SET--" : "");
                }
            }
            return answersData;
        }


        protected object GetAnswerByType(TableReportViewModel answerModel, List<AnswerModel> answerList)
        {
            string answerONQuestion = "--NULL--";
            if (Enum.TryParse(answerModel.InputTypeName, out type))
            {
                switch (type)
                {
                    case QuestionTypes.Textbox:
                        {
                            answerONQuestion = answerList.Count() > 0 ? answerList.FirstOrDefault().AnswerText : answerONQuestion;
                            break;
                        }

                    case QuestionTypes.Textarea:
                        {
                            answerONQuestion = answerList.Count() > 0 ? answerList.FirstOrDefault().AnswerText : answerONQuestion;
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
                            if (answerList.Count() > 0)
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
                            if (answerList.Count() > 0)
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
                            }
                            break;
                        }

                    case QuestionTypes.GridRadio:
                        {
                            answerModel.AdditionalAnswer = answerList.Count() > 0 ? answerList.SingleOrDefault().AnswerText : "";

                            List<TableReportViewModel> tempList = new List<TableReportViewModel>();
                            var rowQuestionList = _rowQuestions.Where(p => p.ParentQuestionId == answerModel.QuestionId).Where(p => p.RespondentId == answerModel.RespondentId).ToList();
                            return AddAnswerToReport(rowQuestionList);
                        }
                }
            }
            return answerONQuestion;
        }
    }
}
