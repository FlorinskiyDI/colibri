using Survey.ApplicationLayer.Dtos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Webapi.Views.Survey
{
    public class SurveyViewModel
    {
        public SurveyModel survey { get; set; }
        public List<Guid> deleteQuestions { get; set; }
    }
}
