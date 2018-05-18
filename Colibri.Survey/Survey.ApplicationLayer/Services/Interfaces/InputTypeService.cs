using System;
using System.Collections.Generic;
using System.Text;
using Survey.ApplicationLayer.Dtos.Entities;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IInputTypeService
    {
        IEnumerable<InputTypesDto> GetAll();
    }
}
