using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace ExceptionHandlingAssignment.Service
{
    public class FileProcess : IFileProcessing
    {
        public bool IsFileFound(string filePath)
        {
            
            try
            {
               

                    using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
                    {
                        WorkbookPart workbookPart = document.WorkbookPart;
                        WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                        SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                        foreach (Row row in sheetData.Elements<Row>())
                        {
                            foreach (Cell cell in row.Elements<Cell>())
                            {

                                string cellValue = GetCellValue(cell, workbookPart);

                            }
                        }

                    }

                
            }
            catch (FileNotFoundException ex)
            {

           return false;
            }
            return true;
        }

        private string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            string value = cell.InnerText;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                int sharedStringId = int.Parse(value);
                SharedStringTablePart stringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                value = stringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(sharedStringId).InnerText;
            }

            return value;
        }


      public  bool IsExcelFile(IFormFile filename)
        {
            string extension = ".xlsx";
            string fileExtension=Path.GetExtension(filename.FileName).ToLower();
           
            return extension == fileExtension ? true : false;

        }
        public bool InsufficientFilePermissions(IFormFile filename)
        {
            string filepath = Path.GetTempFileName();
            using (var stream = new FileStream(filepath, FileMode.Open))
            {
                filename.CopyTo(stream);
                try {
                    using (SpreadsheetDocument document = SpreadsheetDocument.Open(filepath, false))
                    {
                        WorkbookPart workbookPart = document.WorkbookPart;
                        WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                        SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                        foreach (Row row in sheetData.Elements<Row>())
                        {
                            foreach (Cell cell in row.Elements<Cell>())
                            {

                                string cellValue = GetCellValue(cell, workbookPart);

                            }
                        }

                    }


                }
                catch (Exception ex) 
                {
                    return false;
                }

                }
     

            return true;

        }


    }
}
