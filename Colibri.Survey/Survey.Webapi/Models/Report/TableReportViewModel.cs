using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Webapi.Models.Report
{
    public class TableReportViewModel
    {
        public Guid RespondentId { get; set; }
        public Guid PageId { get; set; }
        public Guid QuestionId { get; set; }
        public string QuestionName { get; set; }
        public object Answer { get; set; }


        public TableReportViewModel()
        {
            this.Answer = null;
        }
    }

    
}
