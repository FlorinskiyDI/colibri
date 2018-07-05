using System;
using System.Collections.Generic;
using System.Text;
using Survey.ApplicationLayer.Dtos.Models.Answers;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IAnswerService
    {
        List<BaseAnswerModel> GetTypedAnswerList(List<object> survey);
        void SaveAnswerByType(BaseAnswerModel item);
    }
}
