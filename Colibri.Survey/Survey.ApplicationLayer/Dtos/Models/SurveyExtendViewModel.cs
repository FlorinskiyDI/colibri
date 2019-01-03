using Survey.ApplicationLayer.Dtos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models
{
    public class SurveyExtendViewModel : SurveySectionDto
    {
        public int RespondentsCount { get; set; }
    }
}
