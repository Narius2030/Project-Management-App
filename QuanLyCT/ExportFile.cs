using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace QLCongTy
{
    public class ExportFile
    {
        public void SaveExcel(DataTable dt)
        {
            // Tạo đối tượng Excel
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;

            // Tạo một Workbook mới
            Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = workbook.Sheets[1] as Excel.Worksheet;

            // Lưu dữ liệu từ DataTable vào Excel
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    worksheet.Cells[i + 1, j + 1] = dt.Rows[i][j].ToString();
                }
            }

            // Lưu Workbook ra tệp Excel trên Desktop
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string nameFile = Microsoft.VisualBasic.Interaction.InputBox("Tên File:", "Nhập Tên File", "");
            string filePath = Path.Combine(desktopPath, $"{nameFile}.xlsx");
            workbook.SaveAs(filePath);

            // Đóng Workbook và đối tượng Excel
            workbook.Close();
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(excelApp);
        }
    }
}
