using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services.Interfaces
{

    public interface IServeySectionRespondentServie
    {
        Task<Guid> AddAsync(Guid surveyId, Guid RespondentId);
    }
}
