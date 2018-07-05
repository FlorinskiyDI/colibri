using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models.Answers
{
    public class RowAnswerModel
    {
        public AnswerItemModel Row { get; set; }
        public AnswerItemModel Col { get; set; }
    }
}
