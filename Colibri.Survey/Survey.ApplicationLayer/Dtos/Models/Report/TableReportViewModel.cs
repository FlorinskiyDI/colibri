using System;

namespace Survey.ApplicationLayer.Dtos.Models.Report
{

    public class TableReportViewModel
    {
        public Guid RespondentId { get; set; }
        public Guid QuestionId { get; set; }
        public string QuestionName { get; set; }
        public object Answer { get; set; }
        public string InputTypeName { get; set; }
        public Guid? ParentQuestionId { get; set; }
        public int OrderNo { get; set; }
        public int PageOrderNo { get; set; }

        public TableReportViewModel()
        {
            this.Answer = null;
        }
    }
}
