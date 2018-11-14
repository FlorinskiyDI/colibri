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
        ControlStates state;

        public SurveySectionsController(
            IConfiguration configuration,
            ISurveySectionService surveySectionService,
            IPageService pageService,
        IQuestionService questionService
        )
        {
            _configuration = configuration;
            _surveySectionService = surveySectionService;
            _pageService = pageService;
            _questionService = questionService;
        }


        // GET: api/groups/surveys
        [HttpGet]
        public async Task<IActionResult> Getlist()
        {
            IEnumerable<SurveySectionDto> result;
            try
            {
                result = await _surveySectionService.GetAll();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
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


                    if (data.deleteQuestions.Count > 0)
                    {
                        foreach (var item in data.deleteQuestions)
                        {
                            _questionService.DeleteQuestionById(item);
                        }
                    }
                    if (data.deletePages.Count > 0)
                    {
                        foreach (var item in data.deletePages)
                        {
                            var page = _pageService.GetPageById(item);
                            var questions = _questionService.GetListByPageId(page.Id);
                            if (questions.Count() > 0)
                            {
                                foreach (var question in questions)
                                {
                                    _questionService.DeleteQuestionById(question.Id);
                                }
                            }
                            _pageService.DeletePageById(item);
                        }
                    }


                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                var check = ex;
                throw;
            }
        }
    }
}
