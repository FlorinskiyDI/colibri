using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Survey.ApplicationLayer.Dtos;
using Survey.ApplicationLayer.Services.Interfaces;

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
        public IActionResult GetReport(string surveyId)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var options = _reportService.GetQuestions(Guid.Parse(surveyId));
            var answerList = _reportService.GetAnswersBySurveyId(Guid.Parse(surveyId));

            var sortAnswers = answerList.GroupBy(u => u.RespondentId)
                .Select(grp => new AnswerGroup { DateCreated = grp.First().DateCreated, DataList = grp.ToList() })
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
        public async Task<IActionResult> DownloadGrid(string quizId)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var options = _reportService.GetQuestions(Guid.Parse(quizId));
            var questionList = _reportService.GetAnswersBySurveyId(Guid.Parse(quizId));

            var sortAnswers = questionList.GroupBy(u => u.RespondentId)
                .Select(grp => new AnswerGroup { DateCreated = grp.First().DateCreated, DataList = grp.ToList() })
                .ToList();

            var result = new FileViewModel()
            {
                HeaderOption = options,
                Answers = sortAnswers,
            };

            var exportData = _excelService.ExportExcel(result, "", true);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            var elapsedSec = elapsedMs / 1000;
            return new FileContentResult(exportData.Content, exportData.ContentType);
        }
    }
}
