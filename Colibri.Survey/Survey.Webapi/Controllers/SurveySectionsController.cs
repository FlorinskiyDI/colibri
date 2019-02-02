using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Dtos.Models.Questions;
using Survey.ApplicationLayer.Services;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.Common.Context;
using Survey.Common.Enums;
using Survey.DomainModelLayer.Entities;
using Survey.Webapi.Models.Survey;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Survey.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class SurveySectionsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ISurveySectionService _surveySectionService;
        private readonly IPageService _pageService;
        private readonly IQuestionService _questionService;
        private readonly ISurveySectionRespondentService _surveySectionRespondentServie;
        private readonly IQuestionOptionService _questionOptionService;
        private readonly IAnswerService _answerService;
        ControlStates state;

        public SurveySectionsController(
            ISurveySectionRespondentService surveySectionRespondentServie,
            IConfiguration configuration,
            ISurveySectionService surveySectionService,
            IPageService pageService,
            IAnswerService answerService,
            IQuestionService questionService,
            IQuestionOptionService questionOptionService
        )
        {
            _answerService = answerService;
            _surveySectionRespondentServie = surveySectionRespondentServie;
            _configuration = configuration;
            _surveySectionService = surveySectionService;
            _pageService = pageService;
            _questionService = questionService;
            _questionOptionService = questionOptionService;
        }


        // GET: api/groups/surveys
        [HttpGet]
        public async Task<IActionResult> Getlist()
        {
            IEnumerable<SurveyExtendViewModel> surveys;
            try
            {
                surveys = await _surveySectionService.GetSurveysWithRespondentCount();
                foreach (var item in surveys)
                {
                    item.RespondentsCount = await _surveySectionRespondentServie.GetListBySurveyId(item.Id);
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(surveys);
        }



        //GET: api/surveySections/1
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSurvey(Guid id)
        {
            SurveyModel survey;
            try
            {
                survey = await _surveySectionService.GetAsync(id);
                List<PageModel> pages = await _pageService.GetListBySurvey(Guid.Parse(survey.Id));
                if (pages != null)
                {
                    survey.Pages = pages;
                    foreach (var item in survey.Pages)
                    {
                        var questions = await _questionService.GetTypedQuestionListByPage(item.Id);
                        questions = questions.OrderBy(o => o.Order).ToList();
                        item.Questions.AddRange(questions);
                    }
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(survey);
        }





        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> SaveSurvey([FromBody] SurveyModel survey)
        {
            //await GetSurvey();
            Guid surveyId = await _surveySectionService.AddAsync(survey);
            if (survey.Pages.Count() > 0 && surveyId != null)
            {
                List<BaseQuestionModel> questionList = new List<BaseQuestionModel>();
                foreach (var page in survey.Pages)
                {
                    Guid pageId = await _pageService.AddAsync(page, surveyId);
                    questionList = _questionService.GetTypedQuestionList(page);
                    if (questionList.Count() > 0)
                    {
                        foreach (var question in questionList)
                        {
                            _questionService.SaveQuestionByType(question, pageId);
                        }
                    }
                }
            }
            return Ok();
        }


        [HttpPut]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateSurvey([FromBody] SurveyViewModel data)
        {
            try
            {
                var surveyId = _surveySectionService.Update(data.survey).Result;

                if (data.survey.Pages.Count() > 0)
                {
                    List<BaseQuestionModel> questionList = new List<BaseQuestionModel>();
                    foreach (var page in data.survey.Pages)
                    {
                        if (page.State == ControlStates.Created.ToString())
                        {
                            Guid pageId = await _pageService.AddAsync(page, Guid.Parse(data.survey.Id));
                            questionList = _questionService.GetTypedQuestionList(page);
                            if (questionList.Count() > 0)
                            {
                                foreach (var question in questionList)
                                {
                                    _questionService.SaveQuestionByType(question, pageId);
                                }
                            }
                        }
                        else
                        {
                            Guid pageId = await _pageService.UpdateAsync(page, Guid.Parse(data.survey.Id));
                            questionList = _questionService.GetTypedQuestionList(page);
                            if (questionList.Count() > 0)
                            {
                                _questionService.Update(questionList, page.Id);
                            }
                        }
                    }

                    var deleteQuestionList = data.deleteQuestions.Distinct().ToList();
                    if (deleteQuestionList.Count > 0)
                    {
                        foreach (var item in deleteQuestionList)
                        {
                            var questionOptions = _questionOptionService.GetAllAsync().Result.Where(x => x.QuestionId == item);


                            if (questionOptions.Count() > 0)
                            {
                                foreach (var q_o in questionOptions)
                                {
                                    var answerList = _answerService.GetAllAsync().Result.Where(x => x.QuestionOptionId == q_o.Id);
                                    foreach (var answer in answerList)
                                    {
                                        await _answerService.Remove(answer);
                                    }
                                    await _questionOptionService.Remove(q_o);
                                };

                            }
                            var childQuestions = await _questionService.GetListByBaseQuestion(item);
                            if (childQuestions.Count() > 0)
                            {
                                foreach (var childQuestion in childQuestions)
                                {
                                    var questionOptionList = _questionOptionService.GetAllAsync().Result.Where(x => x.QuestionId == childQuestion.Id);


                                    if (questionOptionList.Count() > 0)
                                    {
                                        foreach (var q_o in questionOptionList)
                                        {
                                            var answerList = _answerService.GetAllAsync().Result.Where(x => x.QuestionOptionId == q_o.Id);
                                            foreach (var answer in answerList)
                                            {
                                                await _answerService.Remove(answer);
                                            }
                                            await _questionOptionService.Remove(q_o);
                                        };

                                    }
                                    await _questionService.DeleteQuestionById(childQuestion.Id);
                                }

                            }
                            await _questionService.DeleteQuestionById(item);
                        }
                    }
                    var deletePageList = data.deletePages.Distinct().ToList();
                    if (deletePageList.Count > 0)
                    {
                        foreach (var pageId in deletePageList)
                        {


                            var questions = _questionService.GetListByPageId(pageId).Select(x => x.Id).ToList();
                            foreach (var question in questions)
                            {
                                var questionOptions = _questionOptionService.GetAllAsync().Result.Where(x => x.QuestionId == question);
                                if (questionOptions.Count() > 0)
                                {
                                    foreach (var q_o in questionOptions)
                                    {
                                        var answerList = _answerService.GetAllAsync().Result.Where(x => x.QuestionOptionId == q_o.Id);
                                        foreach (var answer in answerList)
                                        {
                                            await _answerService.Remove(answer);
                                        }
                                        await _questionOptionService.Remove(q_o);
                                    };
                                }
                                var childQuestions = await _questionService.GetListByBaseQuestion(question);
                                if (childQuestions.Count() > 0)
                                {
                                    foreach (var childQuestion in childQuestions)
                                    {
                                        await _questionService.DeleteQuestionById(childQuestion.Id);
                                    }

                                }
                                await _questionService.DeleteQuestionById(question);
                            }
                            _pageService.DeletePageById(pageId);
                        }
                    }


                }
                return Ok();
            }
            catch (Exception ex)
            {
                var check = ex;
                throw;
            }
        }

        [HttpGet("{surveyId}/{isLocked}")]
        //[Produces("application/json")]
        public async Task<IActionResult> LockSurvey(Guid surveyId, bool isLocked)
        {
            var state = _surveySectionService.SetLockState(surveyId, isLocked).Result;
            return Ok();
        }
    }

}
