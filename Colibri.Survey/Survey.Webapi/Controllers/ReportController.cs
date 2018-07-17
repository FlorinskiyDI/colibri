using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Survey.ApplicationLayer.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Survey.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;



        public ReportController(
            IReportService reportService
            )
        {
            _reportService = reportService;
        }



        [HttpGet]
        [Route("{surveyId}")]
        public async Task<IActionResult> GetReport( string surveyId)
        {
            var check = surveyId.ToString();

            var options = _reportService.GetQuestions(Guid.Parse(surveyId));


            var questionList = _reportService.GetQuesionListBySurveyId(Guid.Parse(surveyId));

            var surveyReport = _reportService.GetReport(questionList);
            var sortAnswers = surveyReport.GroupBy(u => u.RespondentId)
                .Select(grp => grp.ToList())
                .ToList();
            var result = new
            {
                headerOption = options,
                answers = sortAnswers
            };
            return Ok(result);
        }
    }
}
