using Survey.ApplicationLayer.Dtos.Models;
using Survey.DomainModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IPageService
    {
        Task<Guid> AddAsync(PageModel survey, Guid surveyId);
        Task<List<PageModel>> GetListBySurvey(Guid surveyId);
        void DeletePageById(Guid pageId);
        Pages GetPageById(Guid id);
    }
}
