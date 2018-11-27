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

        public static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }


        public static DataTable ObjectListToDataTable(FileViewModel data)
        {
            //PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            //DataTable dataTable = new DataTable();

            //for (int i = 0; i < properties.Count; i++)
            //{
            //    PropertyDescriptor property = properties[i];
            //    dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            //}

            //object[] values = new object[properties.Count];
            //foreach (T item in data)
            //{
            //    for (int i = 0; i < values.Length; i++)
            //    {
            //        values[i] = properties[i].GetValue(item);
            //    }

            //    dataTable.Rows.Add(values);
            //}
            //return dataTable;

            DataTable dataTable = new DataTable();
            try
            {
                var colCount = 0;
                for (int i = 0; i < data.HeaderOption.Count; i++)
                {
                    if (data.HeaderOption[i].Type == QuestionTypes.GridRadio.ToString())
                    {
                        int childNumerator = 1;
                        foreach (var child in data.HeaderOption[i].Children)
                        {
                            dataTable.Columns.Add( "[" + (i + 1) + "." + childNumerator + "] - " + child);
                            colCount = colCount + 1;
                            ++childNumerator;
                        }
                    }
                    var type = data.HeaderOption[i].Type;
                    if (type == QuestionTypes.Textbox.ToString() || type == QuestionTypes.Textarea.ToString())
                    {
                        dataTable.Columns.Add("[" + (i + 1) + "] - " + data.HeaderOption[i].Name);
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

                        if (type == QuestionTypes.GridRadio.ToString())
                        {
                            foreach (var child in groupAnswer[i].Answer as List<TableReportViewModel>)
                            {
                                values[keyValue] = child.Answer;
                                keyValue = keyValue + 1;
                            }
                        }                

                        if (type == QuestionTypes.Textbox.ToString() || type == QuestionTypes.Textarea.ToString())
                        {
                            values[keyValue] = groupAnswer[i].Answer;
                            keyValue = keyValue + 1;
                        }
                    }
                    dataTable.Rows.Add(values);






                    //for (int i = 0; i < data.Answers.Count; i++)
                    //{
                    //    if (null == QuestionTypes.GridRadio.ToString())
                    //    {
                    //        foreach (var child in data.HeaderOption[i].Children)
                    //        {
                    //            dataTable.Columns.Add(i + 1 + " " + child);
                    //        }
                    //    }
                    //    var type = data.HeaderOption[i].Type;
                    //    if (type == QuestionTypes.Textbox.ToString() || type == QuestionTypes.Textarea.ToString())
                    //    {
                    //        dataTable.Columns.Add(i + 1 + " " + data.HeaderOption[i].Name);
                    //    }
                    //}
                }

                var check5 = 5;
            }
            catch (Exception ex)
            {
                var check = ex;
                throw;
            }



            return dataTable;
        }





        public static byte[] ExportExcel(DataTable dataTable, string heading = "", bool showSrNo = false)
        {

            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                int startRowFrom = String.IsNullOrEmpty(heading) ? 2 : 4;

                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("№", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }


                // add the content into the Excel file
                workSheet.Cells["B" + startRowFrom].LoadFromDataTable(dataTable, true);
                //MeargeCells(workSheet, 5, 1, 9, 9);



                //workSheet.Cells[1, 1, 9, 9].Style.WrapText = true;


                // autofit width of cells with small content
                //int columnIndex = 1;
                //foreach (DataColumn column in dataTable.Columns)
                //{
                //    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                //    int maxLength = columnCells.Max(cell => cell.Value.ToString().Count());
                //    if (maxLength < 150)
                //    {
                //        workSheet.Column(columnIndex).AutoFit();
                //    }


                //    columnIndex++;
                //}

                // format header - bold, yellow on black
                int leftIndent = 1;
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1+ leftIndent, startRowFrom, dataTable.Columns.Count + leftIndent])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                    r.Style.WrapText = true;
                    r.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                }

                // format cells - add borders
                //using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                //{
                //    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                //    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                //    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                //    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                //    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                //    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                //    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                //}

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
            fileModel.Content = ExportExcel(ObjectListToDataTable(data), Heading, showSlno);
            fileModel.ContentType = ExcelContentType;

            return fileModel;
        }
    }
}
