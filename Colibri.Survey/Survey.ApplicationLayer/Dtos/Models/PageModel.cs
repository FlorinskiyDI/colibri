using Survey.ApplicationLayer.Dtos.Models.Questions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Models
{
     public class PageModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }

        public IEnumerable<object> Questions { get; set; }

    }
}
