using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.ApplicationLayer.Services
{
    public class SurveyStructureService : ISurveyStructureService
    {
        private readonly IAnswerService _answerService;

        public SurveyStructureService(IAnswerService answerService)
        {
            _answerService = answerService;
        }


        public IEnumerable<Answers> GetAnsersByQuestion_OptionId(Guid id)
        {
            return _answerService.GetAllAsync().Result.Where(x => x.QuestionOptionId == id);
        }


        public void RemoveAnswer(Answers item)
        {
            _answerService.Remove(item);
        }

    }
}
