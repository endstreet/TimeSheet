using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Globalization;
using System.Reflection;
using System.Text;


namespace Trm.MaLogger.App.Services
{
    public static class Extentions
    {
        public static DateTime ToDate(this string date, string fromFormat)
        {
            if (string.IsNullOrEmpty(date)) return DateTime.MinValue;
            return DateTime.ParseExact(date, fromFormat, CultureInfo.InvariantCulture);
        }

        public static DateTime ToDate(this object date, string fromFormat)
        {
            if (date == null) return DateTime.MinValue;
            if (date.GetType().Name == "String")
            {
                return DateTime.ParseExact((string)date, fromFormat, CultureInfo.InvariantCulture);
            }
            return (DateTime)date;
            // 
        }
        /// <summary>
        /// NPOI implimentation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static byte[] ToXlsBytes<T>(this List<T> list)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            IWorkbook workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("TimeSheet");
            //Create Header Row
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex++);
            for (int columnNumber = 0; columnNumber <= properties.Length - 1; columnNumber++)
            {
                var prop = properties[columnNumber];
                row.CreateCell(columnNumber).SetCellValue(prop.Name);
            }

            //Add Rows
            foreach (var item in list)
            {
                row = sheet.CreateRow(rowIndex++);
                for (int columnNumber = 0; columnNumber <= properties.Length - 1; columnNumber++)
                {
                    var prop = properties[columnNumber];
                    row.CreateCell(columnNumber).SetCellValue((prop.GetValue(item) ?? "").ToString());
                }
            }

            using MemoryStream sw = new();
            workbook.Write(sw, false);
            return sw.ToArray();
        }
        public static byte[] ToCsvBytes<T>(this List<T> list)
        {
            StringBuilder sb = new();
            //CreateHeader
            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length - 1; i++)
            {
                sb.Append(properties[i].Name + ",");
            }
            var lastProp = properties[properties.Length - 1].Name;
            sb.Append(lastProp + Environment.NewLine);
            //Rows
            foreach (var item in list)
            {
                properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length - 1; i++)
                {
                    var prop = properties[i];
                    sb.Append(prop.GetValue(item) + ",");
                }
                var llastProp = properties[properties.Length - 1];
                sb.Append(llastProp.GetValue(item) + Environment.NewLine);
            }
            return Encoding.ASCII.GetBytes(sb.ToString());
        }
    }
}
