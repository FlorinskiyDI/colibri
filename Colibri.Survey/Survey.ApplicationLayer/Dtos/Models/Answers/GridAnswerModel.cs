using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models.Answers
{

    public class GridAnswerModel : BaseAnswerModel
    {
        public List<RowAnswerModel> Answer { get; set; }
    }
}
