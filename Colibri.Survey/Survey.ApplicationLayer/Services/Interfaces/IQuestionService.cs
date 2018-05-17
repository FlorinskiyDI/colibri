using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Dtos.Models.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    interface  IQuestionService
    {
        BaseQuestionModel GetQuestionByType(string baseQuestion);
        List<BaseQuestionModel> GetTypedQuestionList(SurveyModel survey);
    }
}
