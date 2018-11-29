using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Survey.ApplicationLayer.Dtos;
using Survey.ApplicationLayer.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Survey.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IExcelService _excelService;



        public ReportController(
             IReportService reportService,
             IExcelService excelService
            )
        {
            _excelService = excelService;
            _reportService = reportService;
        }



        [HttpGet]
        [Route("{surveyId}")]
        public async Task<IActionResult> GetReport(string surveyId)
        {
            var check = surveyId.ToString();

            var options = _reportService.GetQuestions(Guid.Parse(surveyId));

            var watch = System.Diagnostics.Stopwatch.StartNew();



            var answerList = _reportService.GetQuesionListBySurveyId(Guid.Parse(surveyId));

            //var surveyReport = _reportService.GetReport(questionList);
            var sortAnswers = answerList.GroupBy(u => u.RespondentId)
                .Select(grp => grp.ToList())
                .ToList();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            var elapsedSec = elapsedMs / 1000;
            var result = new
            {
                headerOption = options,
                answers = sortAnswers,
                spentTimeMs = "milesec - " + elapsedMs,
                spentTimeSec = "sec - " + elapsedSec
            };
            return Ok(result);
        }


        [HttpPost("DownloadGrid/{quizId}")]
        //[Route("{quizId}")]
        public async Task<IActionResult> DownloadGrid(string quizId)
        {
            var check = quizId.ToString();

            var options = _reportService.GetQuestions(Guid.Parse(quizId));

            var watch = System.Diagnostics.Stopwatch.StartNew();



            var questionList = _reportService.GetQuesionListBySurveyId(Guid.Parse(quizId));

            //var surveyReport = _reportService.GetReport(questionList);
            var sortAnswers = questionList.GroupBy(u => u.RespondentId)
                .Select(grp => grp.ToList())
                .ToList();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            var elapsedSec = elapsedMs / 1000;
            var result = new FileViewModel()
            {
                HeaderOption = options,
                Answers = sortAnswers,
            };

            var exportData = _excelService.ExportExcel(result, "", true);

            return new FileContentResult(exportData.Content, exportData.ContentType);
        }
    }
}
