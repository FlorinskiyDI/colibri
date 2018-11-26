using Survey.ApplicationLayer.Dtos.Models.Report;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos
{
    public class FileViewModel
    {
        public List<ColumModel> HeaderOption { get; set; }
        public List<List<TableReportViewModel>> Answers { get; set; }
    }
}
