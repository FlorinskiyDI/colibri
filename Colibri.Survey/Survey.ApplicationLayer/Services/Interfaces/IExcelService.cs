using Survey.ApplicationLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IExcelService
    {
        FileModel ExportExcel(FileViewModel data, string Heading = "", bool showSlno = false);
    }
}
