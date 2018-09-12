using System;
using System.Collections.Generic;
using System.Text;
using Survey.ApplicationLayer.Dtos.Models.Questions;
using Survey.Common.Enums;

namespace Survey.ApplicationLayer.Dtos.Models
{
    public class PageModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public Guid SurveyId { get; set; }
        //public ICollection<BaseQuestionModel> Qeustions { get; set;
        public List<object> Questions { get; set; }
        public string State { get; set; }
        public  PageModel()
        {
            Questions = new List<object>();
            this.State = ControlStates.Unchanged.ToString();
        }

    }
}
