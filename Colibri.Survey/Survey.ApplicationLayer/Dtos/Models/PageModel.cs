using System;
using System.Collections.Generic;
using System.Text;
using Survey.ApplicationLayer.Dtos.Models.Questions;

namespace Survey.ApplicationLayer.Dtos.Models
{
    public class PageModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderNo { get; set; }
        public Guid SurveyId { get; set; }
        //public ICollection<BaseQuestionModel> Qeustions { get; set;
            public ICollection<object> Questions { get; set; }

    }
}
