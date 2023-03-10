namespace Trm.MaLogger.Data.Views
{
    public class EntryReportSummary
    {

        public string User { get; set; } = null!;
        public string Project { get; set; } = null!;
        public string Client { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double TotalHours { get; set; }
    }
}
