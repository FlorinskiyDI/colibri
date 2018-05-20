using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models
{
    public class SurveyModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<PageModel> Pages { get; set; }
    }
}
