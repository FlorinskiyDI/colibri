using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models.Answers
{
    public class BaseAnswerModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string AdditionalAnswer { get; set; }
        //public object Answer { get; set; }

        public BaseAnswerModel()
        {
            //Answer = new List<object>();
        }
    }
}
