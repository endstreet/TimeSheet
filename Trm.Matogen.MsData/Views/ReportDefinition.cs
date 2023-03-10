namespace Trm.MaLogger.MsData.Views
{
    public class ReportDefinition
    {
        public ReportDefinition(string index)
        {
            Index = index;
        }
        public string Index { get; set; }
        public List<string> sortOrder = new List<string>();
        public List<ReportFilter> Filters = new List<ReportFilter>();
    }
}
