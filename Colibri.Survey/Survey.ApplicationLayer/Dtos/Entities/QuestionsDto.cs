using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos.Entities
{
    public class QuestionsDto
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public int OrderNo { get; set; }
        public bool AdditionalAnswer { get; set; }
        public bool AllowMultipleOptionAnswers { get; set; }

        public Guid InputTypesId { get; set; }
        public Guid PageId { get; set; }
        public Guid? OptionGroupId { get; set; }
    }
}
