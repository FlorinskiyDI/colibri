using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Survey.ApplicationLayer.Dtos.Models.Answers;
using Survey.DomainModelLayer.Entities;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IAnswerService
    {
        Task<Guid> AddAsync(Answers answer);
        List<BaseAnswerModel> GetTypedAnswerList(List<object> survey);
        void SaveAnswerByType(BaseAnswerModel item, Guid id);
    }
}
