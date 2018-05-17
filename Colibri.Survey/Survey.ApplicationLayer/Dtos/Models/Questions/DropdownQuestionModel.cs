using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models.Questions
{
    public class DropdownQuestionModel : BaseQuestionModel
    {
        public List<ItemModel> Options { get; set; }
    }
}
