using Survey.DomainModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface ISurveyStructureService
    {
        IEnumerable<Answers> GetAnsersByQuestion_OptionId(Guid id);
        void RemoveAnswer(Answers item);
    }
}
