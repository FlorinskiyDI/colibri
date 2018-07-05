using Survey.ApplicationLayer.Dtos.Models.Answers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Services.Interfaces
{

    public interface IQuestionOptionService
    {
        Guid Add(Guid questoinId, Guid optionChoiceId);
        //List<BaseAnswerModel> GetTypedAnswerList(List<object> survey);
    }
}
