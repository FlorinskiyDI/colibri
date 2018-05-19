using Survey.ApplicationLayer.Dtos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IOptionChoiceService
    {
        Task AddAsync(Guid optionGroupId, ItemModel item);
    }
}
