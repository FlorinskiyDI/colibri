using OfficeOpenXml;
using OfficeOpenXml.Style;
using Survey.ApplicationLayer.Dtos;
using Survey.ApplicationLayer.Dtos.Models.Report;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Survey.ApplicationLayer.Services
{
    public class ExcelService : IExcelService
    {


        public static string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }

        //public static DataTable ListToDataTable<T>(List<T> data)
        //{
        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        //    DataTable dataTable = new DataTable();

        //    for (int i = 0; i < properties.Count; i++)
        //    {
        //        PropertyDescriptor property = properties[i];
        //        dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
        //    }

        //    object[] values = new object[properties.Count];
        //    foreach (T item in data)
        //    {
        //        for (int i = 0; i < values.Length; i++)
        //        {
        //            values[i] = properties[i].GetValue(item);
        //        }

        //        dataTable.Rows.Add(values);
        //    }
        //    return dataTable;
        //}


        public static DataTable ObjectListToDataTable(FileViewModel data)
        {
            DataTable dataTable = new DataTable();

            var colCount = 0;
            for (int i = 0; i < data.HeaderOption.Count; i++)
            {
                var type = data.HeaderOption[i].Type;
                if (type == QuestionTypes.GridRadio.ToString())
                {
                    int childNumerator = 1;
                    foreach (var child in data.HeaderOption[i].Children)
                    {
                        dataTable.Columns.Add("[" + (i + 1) + "." + childNumerator + "] - " + child);
                        colCount = colCount + 1;
                        ++childNumerator;
                    }
                }
                if (
                    type == QuestionTypes.Textbox.ToString() ||
                    type == QuestionTypes.Textarea.ToString() ||
                    type == QuestionTypes.Radio.ToString() ||
                    type == QuestionTypes.Checkbox.ToString() ||
                    type == QuestionTypes.Dropdown.ToString())
                {
                    dataTable.Columns.Add("[" + (i + 1) + "] - " + data.HeaderOption[i].Name);
                    colCount = colCount + 1;
                }
                if (data.HeaderOption[i].AdditionalAnswer)
                {
                    dataTable.Columns.Add("[" + (i + 1) + "] - Additional answer");
                    colCount = colCount + 1;
                }
            }


            foreach (var groupAnswer in data.Answers)
            {
                object[] values = new object[colCount];
                int keyValue = 0;
                for (int i = 0; i < groupAnswer.Count; i++)
                {
                    var type = groupAnswer[i].InputTypeName;


                    // grid answer
                    if (type == QuestionTypes.GridRadio.ToString())
                    {
                        foreach (var child in groupAnswer[i].Answer as List<TableReportViewModel>)
                        {
                            var answerString = child.Answer as string;
                            values[keyValue] = String.IsNullOrEmpty(answerString) ? "NULL" : answerString;
                            keyValue = keyValue + 1;
                        }
                    }

                    // textbox, textarea, radio, checkbox, dropdown answer
                    if (
                         type == QuestionTypes.Textbox.ToString() ||
                    type == QuestionTypes.Textarea.ToString() ||
                    type == QuestionTypes.Radio.ToString() ||
                    type == QuestionTypes.Checkbox.ToString() ||
                    type == QuestionTypes.Dropdown.ToString())
                    {
                        values[keyValue] = groupAnswer[i].Answer;
                        keyValue = keyValue + 1;
                    }
                    if (!String.IsNullOrEmpty(groupAnswer[i].AdditionalAnswer))
                    {
                        values[keyValue] = groupAnswer[i].AdditionalAnswer;
                        keyValue = keyValue + 1;
                    }
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }





        public static byte[] ExportToExcel(FileViewModel data, string heading = "", bool showSrNo = false)
        {
            var dataTable = ObjectListToDataTable(data);
            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                int startRowFrom = String.IsNullOrEmpty(heading) ? 3 : 5;



                // add parent column
                List<CellPosition> cellList = new List<CellPosition>();
                DataTable headerColumn = new DataTable();
                var headerColCount = 0;
                var firstCell = startRowFrom - 1;
                for (int i = 0; i < data.HeaderOption.Count; i++)
                {

                    var item = data.HeaderOption[i];
                    // grid answer
                    if (item.Type == QuestionTypes.GridRadio.ToString())
                    {
                        headerColumn.Columns.Add("[" + (i + 1) + "] - " + item.Name);
                        var count = item.Children.Count;
                        int checkcount = 1;
                        for (int j = 0; j < (count - 1); j++)
                        {
                            try
                            {
                                
                                     headerColumn.Columns.Add("[" + (i + 1) + "." + checkcount + "] - " + item.Children[j]);
                                //headerColumn.Columns.Add(Guid.NewGuid().ToString());
                                ++checkcount;
                            }
                            catch (Exception ex)
                            {
                                var check5 = ex;
                                throw;
                            }

                        }
                        ++headerColCount;

                        cellList.Add(new CellPosition()
                        {
                            FromRow = startRowFrom - 1,
                            FromColumn = firstCell,
                            ToRow = startRowFrom - 1,
                            ToColumn = firstCell + count - 1
                        }
                         );

                        firstCell = firstCell + count;
                    }

                    // textbox, textarea, radio, checkbox, dropdown answer
                    if (
                         item.Type == QuestionTypes.Textbox.ToString() ||
                        item.Type == QuestionTypes.Textarea.ToString() ||
                        item.Type == QuestionTypes.Radio.ToString() ||
                        item.Type == QuestionTypes.Checkbox.ToString() ||
                        item.Type == QuestionTypes.Dropdown.ToString())
                    {
                        headerColumn.Columns.Add("[" + (i + 1) + "] - " + item.Name);
                        ++headerColCount;

                        cellList.Add(new CellPosition()
                        {
                            FromRow = startRowFrom - 1,
                            FromColumn = firstCell,
                            ToRow = startRowFrom,
                            ToColumn = firstCell
                        }
                        );
                        ++firstCell;
                    }
                    if (data.HeaderOption[i].AdditionalAnswer)
                    {
                        headerColumn.Columns.Add("[" + (i + 1) + " - ADDITIONAL ANSWER]");
                        ++headerColCount;

                        cellList.Add(new CellPosition()
                        {
                            FromRow = startRowFrom - 1,
                            FromColumn = firstCell,
                            ToRow = startRowFrom,
                            ToColumn = firstCell
                        }
                        );
                        ++firstCell;
                    }

                }

                // not work !!!!
                //if (showSrNo)
                //{
                //    DataColumn dataColumn = dataTable.Columns.Add("№", typeof(int));
                //    dataColumn.SetOrdinal(0);
                //    int index = 1;
                //    foreach (DataRow item in dataTable.Rows)
                //    {
                //        item[0] = index;
                //        index++;
                //    }
                //}

                // add the header content into the Excel file
                //workSheet.Cells[3, 2, 3, 2].LoadFromDataTable(headerColumn, true); // start header from
                //workSheet.Cells["A" + (startRowFrom - 1)].LoadFromDataTable(headerColumn, true); // start header from

                workSheet.Cells[(startRowFrom - 1), 2, (startRowFrom - 1), 2].LoadFromDataTable(headerColumn, true); // start header from (B2)
                // add the content into the Excel file
                workSheet.Cells[startRowFrom, 2, startRowFrom, 2].LoadFromDataTable(dataTable, true); // start table from  (B3)
                                                                                                      //MeargeCells(workSheet, 2, 2, 2, 3);

                for (int i = 2; i < (dataTable.Columns.Count + 2); i++)
                {
                    workSheet.Column(i).AutoFit();
                    //workSheet.Column(i).Width = 30;
                }
                foreach (var item in cellList)
                {
                    MeargeCells(workSheet, item.FromRow, item.FromColumn, item.ToRow, item.ToColumn);
                }

                //workSheet.Cells[1, 1, 9, 9].Style.WrapText = true;






                // autofit width of cells with small content
                //int columnIndex = 1;
                //foreach (DataColumn column in dataTable.Columns)
                //{
                //    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex + 1, workSheet.Dimension.End.Row, columnIndex + 1];
                //    //ExcelRange columnCells = workSheet.Cells[startRowFrom - 1, 1 + columnIndex, startRowFrom, dataTable.Columns.Count + columnIndex];
                //    int maxLength = columnCells.Max(cell => cell.Value.ToString().Count());
                //    //workSheet.Column(columnIndex).Width =100;
                //    var item = workSheet.Column(columnIndex + 1);

                //    if (maxLength < 150)
                //    {
                //        workSheet.Column(columnIndex).Width = 100;
                //        //workSheet.Column(columnIndex).AutoFit();
                //    }
                //    //workSheet.Column(columnIndex).Width = 30;
                //    //else
                //    //{
                //    //    workSheet.Column(columnIndex).Width = 300;
                //    //}
                //    //workSheet.Column(columnIndex).AutoFit();

                //    columnIndex++;
                //}
               



                // format header - bold, yellow on black
                int leftIndent = 1;
                using (ExcelRange r = workSheet.Cells[startRowFrom - 1, 1 + leftIndent, startRowFrom, dataTable.Columns.Count + leftIndent])
                {

                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#a2c4c9"));
                    // add border style
                    r.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    // add border color
                    r.Style.Border.Top.Color.SetColor(Color.Black);
                    r.Style.Border.Top.Color.SetColor(Color.Black);
                    r.Style.Border.Top.Color.SetColor(Color.Black);
                    r.Style.Border.Top.Color.SetColor(Color.Black);

                    r.Style.WrapText = true;
                    r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                using (ExcelRange rr = workSheet.Cells[startRowFrom + 1, 1 + leftIndent, dataTable.Rows.Count + startRowFrom, dataTable.Columns.Count + leftIndent])
                {

                    rr.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    rr.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#d0e0e3"));
                    // add border style
                    rr.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rr.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rr.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rr.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    // add border color
                    rr.Style.Border.Top.Color.SetColor(Color.Black);
                    rr.Style.Border.Top.Color.SetColor(Color.Black);
                    rr.Style.Border.Top.Color.SetColor(Color.Black);
                    rr.Style.Border.Top.Color.SetColor(Color.Black);

                    rr.Style.WrapText = true;
                    rr.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                }


                //// removed ignored columns
                //for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
                //{
                //    if (i == 0 && showSrNo)
                //    {
                //        continue;
                //    }
                //    if (!columnsToTake.Contains(dataTable.Columns[i].ColumnName))
                //    {
                //        workSheet.DeleteColumn(i + 1);
                //    }
                //}

                if (!String.IsNullOrEmpty(heading))
                {
                    workSheet.Cells["A1"].Value = heading;
                    workSheet.Cells["A1"].Style.Font.Size = 20;

                    workSheet.InsertColumn(1, 1);
                    workSheet.InsertRow(1, 1);
                    workSheet.Column(1).Width = 5;
                }
              


                result = package.GetAsByteArray();
            }

            return result;
        }


        public static void MeargeCells(ExcelWorksheet worksheet, int FromRow, int FromColumn, int ToRow, int ToColumn)
        {
            worksheet.Cells[FromRow, FromColumn, ToRow, ToColumn].Merge = true;
        }



        public FileModel ExportExcel(FileViewModel data, string Heading = "", bool showSlno = false)
        {
            FileModel fileModel = new FileModel();
            fileModel.Content = ExportToExcel(data, Heading, showSlno);
            fileModel.ContentType = ExcelContentType;

            return fileModel;
        }
    }
}
