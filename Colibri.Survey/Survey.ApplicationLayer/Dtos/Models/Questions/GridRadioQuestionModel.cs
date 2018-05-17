using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models.Questions
{
    class GridRadioQuestionModel 
    {
        public List<ItemModel>  Rows { get; set; }
        public List<ItemModel> Cols { get; set; }
    }
}
