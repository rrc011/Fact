using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.AspNetCore.Http;

namespace Utilities
{
    public static class ExcelManagement
    {
        public static DataTable getDataTableFromExcel(string path)
        {
            using (var pck = new ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                bool hasHeader = true; // adjust it accordingly( i've mentioned that this is a simple approach)

                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));

                var startRow = hasHeader ? 2 : 1;
                for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    var row = tbl.NewRow();

                    foreach (var cell in wsRow)
                        row[cell.Start.Column - 1] = cell.Text;

                    tbl.Rows.Add(row);
                }
                return tbl;
            }
        }

        public static byte[] ListToExcel<T>(List<T> query)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Result");

                //get our column headings
                var t = typeof(T);
                var Headings = t.GetProperties();

                for (int i = 0; i < Headings.Count(); i++)
                    ws.Cells[1, i + 1].Value = Headings[i].Name;

                //populate our Data
                if (query.Count() > 0)
                    ws.Cells["A2"].LoadFromCollection(query);

                //Format the header
                // using (ExcelRange rng = ws.Cells["A1:BZ1"])
                // {
                // rng.Style.Font.Bold = true;
                // rng.Style.Fill.PatternType = ExcelFillStyle.Solid;  //Set Pattern for the background to Solid
                // rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(79, 129, 189));  //Set color to dark blue
                // rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
                // }

                //Write it back to the client
                //HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //HttpContext.Response.AddHeader("content-disposition", "attachment;  filename=Resultado.xlsx");
                //HttpContext.Response.BinaryWrite(pck.GetAsByteArray());
                //HttpContext.Response.End();

                return pck.GetAsByteArray();
            }
        }
    }
}