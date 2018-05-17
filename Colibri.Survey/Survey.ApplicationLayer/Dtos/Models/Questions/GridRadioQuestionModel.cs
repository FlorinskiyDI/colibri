using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models.Questions
{
    public class GridRadioQuestionModel : BaseQuestionModel 
    {
    public GridOptionsModel Grid { get; set; }
    }
}
