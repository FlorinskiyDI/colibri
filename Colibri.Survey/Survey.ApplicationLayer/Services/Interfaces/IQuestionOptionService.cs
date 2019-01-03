using Survey.ApplicationLayer.Dtos.Models.Answers;
using Survey.DomainModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services.Interfaces
{

    public interface IQuestionOptionService
    {
        Guid Add(Guid questoinId, Guid optionChoiceId);
        Task<IEnumerable<QuestionOptions>> GetAllAsync();
        Task Remove(QuestionOptions q_o);
    }
}
