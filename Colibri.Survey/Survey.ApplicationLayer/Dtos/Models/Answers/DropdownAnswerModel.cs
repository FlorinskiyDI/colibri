using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models.Answers
{

    public class DropdownAnswerModel : BaseAnswerModel
    {
        public AnswerItemModel Answer { get; set; }
    }
}
