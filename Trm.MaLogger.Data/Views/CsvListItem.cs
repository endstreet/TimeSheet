namespace Trm.MaLogger.Data.Views
{
    public class CsvListItem
    {
        public CsvListItem(string fileName)
        {
            List<string> fields = fileName.Split("-").ToList();
            FileName = fileName;
            UserName = fields.Skip(0).First();
            ReportName = fields.Skip(1).First();
            ReportDate = new DateTime(int.Parse(fields.Skip(2).First()), int.Parse(fields.Skip(3).First()), int.Parse(fields.Skip(4).First()), int.Parse(fields.Skip(5).First()), int.Parse(fields.Skip(6).First().Substring(0, 2)), 0);
        }
        public string UserName { get; set; }
        public string ReportName { get; set; }
        public string FileName { get; set; }
        public DateTime? ReportDate { get; set; }
    }
}
