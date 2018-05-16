using Survey.ApplicationLayer.Dtos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface ISurveySectionService
    {
        IEnumerable<SurveySectionDto> GetAll();
    }
}
