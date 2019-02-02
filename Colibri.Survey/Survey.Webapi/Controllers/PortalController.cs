using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Dtos.Models.Answers;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.Webapi.Models.Portal;

namespace Survey.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class PortalController : Controller
    {
        private readonly ISurveySectionRespondentService _surveySectionRespondentService;
        private readonly ISurveySectionService _surveySectionService;
        private readonly IPageService _pageService;
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        private readonly IRespondentService _respondentService;

        public static string respondentId = null;

        public PortalController(
            ISurveySectionService surveySectionService,
            IPageService pageService,
            IQuestionService questionService,
            IAnswerService answerService,
            IRespondentService respondentService,
            ISurveySectionRespondentService surveySectionRespondentService
        )
        {
            _surveySectionService = surveySectionService;
            _pageService = pageService;
            _questionService = questionService;
            _answerService = answerService;
            _respondentService = respondentService;
            _surveySectionRespondentService = surveySectionRespondentService;
        }


        [HttpGet]
        public async Task<IActionResult> Getlist()
        {
            IEnumerable<SurveySectionDto> result;
            result = await _surveySectionService.GetUnlockedSuerveys();
            return Ok(result);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSurvey(Guid id)
        {
            var survey = await _surveySectionService.GetAsync(id);
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
            return Ok(survey);
        }



        [HttpPost]
        public async Task<IActionResult> SaveAnswers([FromBody] AnswersViewModel respondentData)
        {
            Guid respondentId = await _respondentService.AddAsync();

            await _surveySectionRespondentService.AddAsync(respondentData.SurveyId, respondentId);
            List<BaseAnswerModel> typedAnswerList = new List<BaseAnswerModel>();
            typedAnswerList = _answerService.GetTypedAnswerList(respondentData.AnswerList);
            if (typedAnswerList.Any())
            {
                foreach (var item in typedAnswerList)
                {
                    var question = _questionService.GetQuestionById(item.Id);
                    item.OptionGroupId = question.OptionGroupId.Value;
                    _answerService.SaveAnswerByType(item, respondentId);
                }
            }
            PortalController.respondentId = respondentId.ToString();
            return Ok();
        }
    }
}
