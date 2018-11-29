using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Dtos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface ISurveySectionService
    {
        Task<IEnumerable<SurveySectionDto>> GetAll();
        Task<Guid> AddAsync(SurveyModel dto);
        Task<SurveyModel> GetAsync(Guid surveyId);
        Task<Guid> Update(SurveyModel survey);
        Task<bool> SetLockState(Guid id, bool state);
        Task<IEnumerable<SurveySectionDto>> GetUnlockedSuerveys();
    }
}
