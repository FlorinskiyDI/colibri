using Survey.ApplicationLayer.Dtos.Models.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IReportService
    {
        List<ColumModel> GetQuestions(Guid surveyId);
        List<List<TableReportViewModel>> GetQuesionListBySurveyId(Guid surveyId);

        List<TableReportViewModel> GetReport(List<List<TableReportViewModel>> answersData);

        //Task<IEnumerable<>> GetAll();
    }
}
