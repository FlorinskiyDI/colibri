using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IRespondentService
    {
        Task<Guid> AddAsync();
    }
}
