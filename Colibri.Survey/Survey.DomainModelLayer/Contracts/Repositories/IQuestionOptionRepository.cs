using storagecore.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Survey.DomainModelLayer.Entities;

namespace Survey.DomainModelLayer.Contracts.Repositories
{

    public interface IQuestionOptionRepository : IBaseRepository<QuestionOptions, Guid>
    {
    }
}
