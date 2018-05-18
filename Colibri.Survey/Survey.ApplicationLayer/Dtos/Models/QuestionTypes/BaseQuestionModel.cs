using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Models.QuestionTypes.Models
{
    public class BaseQuestionModel
    {
        public Guid Id { get; set; }
        public object Value { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public int Order { get; set; }
        public string ControlType { get; set; }
        public bool IsAdditionalAnswer { get; set; }
    }
}
