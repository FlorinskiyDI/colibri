using Survey.ApplicationLayer.Dtos.Models.Answers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Services.Interfaces
{

    public interface IQuestionOptionService
    {
        Guid Add(BaseAnswerModel item, Guid optionChoiceId);
        //List<BaseAnswerModel> GetTypedAnswerList(List<object> survey);
    }
}
