using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Entities
{
    public class PagesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderNo { get; set; }
        public Guid SurveyId { get; set; }
        public List<QuestionsDto> Questions { get; set; }
    }
}
