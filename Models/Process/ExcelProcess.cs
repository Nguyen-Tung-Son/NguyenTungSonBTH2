/*using System.Data;
using OfficeOpenXml;
namespace DemoMVC2.Models.Process
{
    public class ExcelProcess
    {
        public DataTable ExcelToDataTable(String strPath)
        {
            FileInfo fi = new FileInfo(strPath);
            ExcelPackage excelPackage = new ExcelPackage(fi);
            DataTable dt = new DataTable();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
            //check if the worksheet is completely empty
            if (worksheet.Dimension == null)
            {
                return dt;
            }
            //create a List to hold the column names
            List<string> columnNames = new List<string>();
            //need to keep track of empty column headers
            int currentColumn = 1;
            //loop all columns in the sheet and add them to the datatable
            foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                string columnName = cell.Text.Trim();
                //check if the previous header wasnempty and add it if it was
                if (cell.Start.Column != currentColumn)
                {
                    columnNames.Add("Header_" + currentColumn);
                    dt.Columns.Add("Header_" + currentColumn);
                    currentColumn++;
                }
                //add the column name to the list to count the dublicates
                columnNames.Add(columnName);
                //count the dublicate column names and make them unique to avoid the exception
                //A column named 'Name' already belongs to this DataTable
                int occurences = columnNames.Count(x => x.Equals(columnName));
                if (occurences > 1)
                {
                    columnName = columnName + "_" + occurences;
                }
                //add the column to the database
                dt.Columns.Add(columnName);
                currentColumn++;
            }
            //start adding the contents of the excel file to the database
            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
            {
                var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                DataRow newRow = dt.NewRow();
                //loop all cells in the row
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                dt.Rows.Add(newRow);
            }
            return dt;
        }
    }
}*/
using System.Data;
using OfficeOpenXml;
namespace DemoMVC2.Models.Process
{
    public class ExcelProcess
    {
        public DataTable ExcelToDataTable(string strPath)
        {
            FileInfo fi = new FileInfo(strPath);
            ExcelPackage excelPackage = new ExcelPackage(fi);
            DataTable dt = new DataTable();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
            if (worksheet.Dimension == null)
            {
                return dt;
            }
            List<string> columnNames = new List<string>();
            int currentColumn = 1;
            foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                string columnName = cell.Text.Trim();
                if (cell.Start.Column != currentColumn)
                {
                    columnNames.Add("Header_" + currentColumn);
                    dt.Columns.Add("Header_" + currentColumn);
                    currentColumn++;
                }
                // thêm tên cột vào danh sách để đếm các bản sao 
                columnNames.Add(columnName);
                // đếm các tên cột trùng lặp và đặt chúng thành duy nhất để tránh tồn tại 
                // Một cột có tên 'Tên' đã thuộc về DataTable này 
                int occurrences = columnNames.Count(x => x.Equals(columnName));
                if (occurrences > 1)
                {
                    columnName = columnName + "" + occurrences;
                }
                // thêm cột vào datatable 
                dt.Columns.Add(columnName);
                currentColumn++;
            }
            // bắt đầu thêm nội dung của tệp excel vào dữ liệu cho 
            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
            {
                var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                DataRow newRow = dt.NewRow();
                // lặp tất cả các ô trong hàng 
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }

                dt.Rows.Add(newRow);
            }
            return dt;
        }
    }

}