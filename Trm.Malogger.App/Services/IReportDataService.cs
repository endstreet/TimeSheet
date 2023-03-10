using Trm.MaLogger.MsData.Models;
using Trm.MaLogger.MsData.Views;

namespace Trm.MaLogger.App.Services
{
    public interface IReportDataService
    {
        string GetFileName(string reportIndex, string userName, string fileFormat);
        Task<Report> GetReport(string reportName);
        byte[] GetReportContent(ActiveUser user, ReportDefinition report, string format);
        Dictionary<string, string> MonthList();
        void SaveReport(Report report);
    }
}