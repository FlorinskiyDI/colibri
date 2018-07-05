using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models.Answers
{

    public class CheckBoxAnswerModel : BaseAnswerModel
    {
        public List<string> Answer { get; set; }
    }
}
