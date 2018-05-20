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


        //GET: api/surveySections/1
        [HttpGet]
        public async Task<IActionResult> GetSurvey()
        {
            Guid userId = Guid.Parse(NTContext.Context.UserId);
            SurveyModel survey;
            try
            {
                survey = await _surveySectionService.GetByUser(userId);
                List<PageModel> pages = await _pageService.GetListBySurvey(Guid.Parse(survey.Id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(survey);
        }





        [HttpPost]
        [Produces("application/json")]
        public async  Task<IActionResult> SaveSurvey( [FromBody] SurveyModel survey)
        {
            await GetSurvey();
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
    }
}
