using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.Globalization;
//using System.Linq.Dynamic.Core;
using System.Reflection;
using Trm.MaLogger.MsData.Context;
using Trm.MaLogger.MsData.Models;
using Trm.MaLogger.MsData.Views;

namespace Trm.MaLogger.App.Services
{
    public class ReportDataService : IReportDataService
    {

        public Dictionary<string, string> reportList;
        private Dictionary<string, string> _months;
        readonly MaLoggerDbContext _context;
        readonly StaticDataService _sd;
        public ReportDataService(MaLoggerDbContext context, StaticDataService sd)
        {
            _context = context;
            _sd = sd;
            reportList = new Dictionary<string, string>
            {
                { "1", "TimeSheet_Data" },
                { "2", "TimeSheet_Summary" }
            };
            _months = MonthList();
        }


        public async void SaveReport(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task<Report> GetReport(string reportName)
        {
            try
            {
                return await _context.Reports.Where(r => r.ReportName == reportName).FirstAsync();
            }
            catch
            {
                throw new Exception($"Report {reportName} not found");
            }
        }

        //public void SaveReport(ActiveUser user, ReportDefinition report, string format = "csv")
        //{
        //    switch (report.Index)
        //    {
        //        case "1"://Timesheet Entries
        //            List<EntryReport> result = GetPeriodEntries(user, report);
        //            result.ToFile(GetFileName(report.Index, user.Name), reportFolder, format);
        //            break;
        //        case "2"://Timesheet Summary(user, report);
        //            List<EntryReportSummary> summary = GetPeriodEntrySummary(user, report);
        //            summary.ToFile(GetFileName(report.Index, user.Name), reportFolder, format);
        //            break;
        //        default:
        //            break;
        //    }

        //}

        //public void SaveReportToDb(ActiveUser user, ReportDefinition report, string format = "csv")
        //{
        //    switch (report.Index)
        //    {
        //        case "1"://Timesheet Entries
        //            List<EntryReport> result = GetPeriodEntries(user, report);
        //            result.ToFile(GetFileName(report.Index, user.Name), reportFolder, format);
        //            break;
        //        case "2"://Timesheet Summary(user, report);
        //            List<EntryReportSummary> summary = GetPeriodEntrySummary(user, report);
        //            summary.ToFile(GetFileName(report.Index, user.Name), reportFolder, format);
        //            break;
        //        default:
        //            break;
        //    }

        //}

        public byte[] GetReportContent(ActiveUser user, ReportDefinition report, string format)
        {

            switch (report.Index)
            {
                case "1"://Timesheet Entries
                    List<EntryReport> result = GetPeriodEntries(report);
                    //if (format == "csv")
                    //{
                    //    return result.ToCsvBytes();//GetFileName(report.Index, user.Name), reportFolder, format);
                    //}
                    return result.ToXlsBytes();
                case "2"://Timesheet Summary(user, report);
                    return GetPeriodSummary(report);
                //if (format == "csv")
                //{
                //    return summary.ToCsvBytes();//GetFileName(report.Index, user.Name), reportFolder, format);
                //}
                //This will return all the columns
                //return summary.ToXlsBytes();//summary.ToFile(GetFileName(report.Index, user.Name), reportFolder, format);
                default:
                    throw new Exception("Won't happen exception");
            }

        }
        /// <summary>
        /// Creates a formatted filename
        /// be careful properies are parsed from physical files named using this method.
        /// </summary>
        /// <param name="reportIndex"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetFileName(string reportIndex, string userName, string fileFormat)
        {
            return $"{userName.Replace(' ', '_').ToUpper()}-{reportList[reportIndex]}-{DateTime.Now.ToString("yyyy-MM-dd").Replace("/", "-")}-{DateTime.Now:HH-mm}.{fileFormat}";
        }
        private List<EntryReport> GetPeriodEntries(ReportDefinition report)
        {
            var _entries = GetFilterdQuery(report.Filters).ToList();

            var result = (from t in _entries
                          join u in _context.Users on t.UserId equals u.Id
                          join p in _sd.Projects on t.ProjectId equals p.Id
                          join c in _sd.Clients on p.ClientId equals c.Id

                          select new EntryReport()
                          {
                              User = u.Name,
                              Description = t.Description,
                              Project = p.Name,
                              Client = c.Name,
                              StartTime = t.StartTime,
                              EndTime = t.EndTime
                          }).ToList();

            foreach (string sortColumn in report.sortOrder)
            {
                switch (sortColumn)
                {
                    case "StartDate":
                        result = result.OrderByDescending(t => t.StartTime).ToList();
                        break;
                    case "EndDate":
                        result = result.OrderByDescending(t => t.EndTime).ToList();
                        break;
                    case "Project":
                        result = result.OrderBy(t => t.Project).ToList();
                        break;
                    case "Client":
                        result = result.OrderBy(t => t.Client).ToList();
                        break;
                    case "User":
                        result = result.OrderBy(t => t.User).ToList();
                        break;
                    case "Description":
                        result = result.OrderBy(t => t.Description).ToList();
                        break;
                }
            }
            return result;
        }

        //SortingHelper.SortByClause(list, SortingParms).ToList();
        private byte[] GetPeriodSummary(ReportDefinition report)
        {

            var query = GetFilterdQuery(report.Filters).ToList();
            //Join User frienly column values
            var result = (from t in query
                          join u in _context.Users on t.UserId equals u.Id
                          join p in _sd.Projects on t.ProjectId equals p.Id
                          join c in _sd.Clients on p.ClientId equals c.Id
                          select new EntryReport()
                          {
                              User = u.Name,
                              Description = t.Description,
                              Project = p.Name,
                              Client = c.Name,
                              StartTime = t.StartTime,
                              EndTime = t.EndTime
                          }).ToList();

            //List<EntryReport> sel = result.AsQueryable().GroupByMany(report.sortOrder.ToArray()).ToList();

            //    .Select(x => new EntryReportSummary
            //{
            //    User = x.Key.User,
            //    Client = x.Key.Client,
            //    Project = x.Key.Project,
            //    Description = x.Key.Description,
            //    TotalHours = Math.Round(x.Subgroups.Sum(x => (x.).TotalHours), 2)
            //}).ToList();

            //SortingHelper.SortByClause(result, report.sortOrder).ToList();

            List<EntryReportSummary> summary = new();

            if (report.sortOrder.Contains("User") && report.sortOrder.Contains("Client") && report.sortOrder.Contains("Project") && report.sortOrder.Contains("Description"))
            {
                summary = result.OrderBy(r => r.User).ThenBy(r => r.Client).ThenBy(r => r.Project).ThenBy(r => r.Description).GroupBy(x => new { x.User, x.Client, x.Project, x.Description })
                .Select(x => new EntryReportSummary
                {
                    User = x.Key.User,
                    Client = x.Key.Client,
                    Project = x.Key.Project,
                    Description = x.Key.Description,
                    TotalHours = Math.Round(x.Sum(x => x.Hours), 2)
                }).ToList();
            }
            else if (report.sortOrder.Contains("User") && report.sortOrder.Contains("Client") && report.sortOrder.Contains("Project"))
            {
                summary = result.OrderBy(r => r.User).ThenBy(r => r.Client).ThenBy(r => r.Project).GroupBy(x => new { x.User, x.Client, x.Project })
                .Select(x => new EntryReportSummary
                {
                    User = x.Key.User,
                    Client = x.Key.Client,
                    Project = x.Key.Project,
                    TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
                }).ToList();
            }
            else if (report.sortOrder.Contains("User") && report.sortOrder.Contains("Client"))
            {
                summary = result.OrderBy(r => r.User).ThenBy(r => r.Client).GroupBy(x => new { x.User, x.Client })
                .Select(x => new EntryReportSummary
                {
                    User = x.Key.User,
                    Client = x.Key.Client,
                    TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
                }).ToList();
            }
            else if (report.sortOrder.Contains("User"))
            {
                summary = result.OrderBy(r => r.User).GroupBy(x => new { x.User })
                .Select(x => new EntryReportSummary
                {
                    User = x.Key.User,
                    TotalHours = Math.Round(x.Sum(x => x.Hours), 2)
                }).ToList();
            }
            else if (report.sortOrder.Contains("Client") && report.sortOrder.Contains("Project") && report.sortOrder.Contains("Description"))
            {
                summary = result.OrderBy(r => r.Client).ThenBy(r => r.Project).ThenBy(r => r.Description).GroupBy(x => new { x.Client, x.Project, x.Description })
                .Select(x => new EntryReportSummary
                {
                    Client = x.Key.Client,
                    Project = x.Key.Project,
                    Description = x.Key.Description,
                    TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
                }).ToList();
            }
            else if (report.sortOrder.Contains("Client") && report.sortOrder.Contains("Project"))
            {
                summary = result.OrderBy(r => r.Client).ThenBy(r => r.Project).GroupBy(x => new { x.Client, x.Project })
                .Select(x => new EntryReportSummary
                {
                    Client = x.Key.Client,
                    Project = x.Key.Project,
                    TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
                }).ToList();
            }
            else if (report.sortOrder.Contains("Client"))
            {
                summary = result.OrderBy(r => r.Client).GroupBy(x => new { x.Client })
                .Select(x => new EntryReportSummary
                {
                    Client = x.Key.Client,
                    TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
                }).ToList();
            }
            else if (report.sortOrder.Contains("Project") && report.sortOrder.Contains("Description"))
            {
                summary = result.OrderBy(r => r.Project).ThenBy(r => r.Description).GroupBy(x => new { x.Project, x.Description })
                .Select(x => new EntryReportSummary
                {
                    Project = x.Key.Project,
                    Description = x.Key.Description,
                    TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
                }).ToList();
            }
            else if (report.sortOrder.Contains("Project"))
            {
                summary = result.OrderBy(r => r.Project).GroupBy(x => new { x.Project })
                .Select(x => new EntryReportSummary
                {
                    Project = x.Key.Project,
                    TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
                }).ToList();
            }
            else if (report.sortOrder.Contains("Decription"))
            {
                summary = result.OrderBy(r => r.Description).GroupBy(x => new { x.Description })
                .Select(x => new EntryReportSummary
                {
                    Description = x.Key.Description,
                    TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
                }).ToList();
            }

            //Greate Xcel Image
            report.sortOrder.Add("TotalHours");

            PropertyInfo[] properties = typeof(EntryReportSummary).GetProperties();
            IWorkbook workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("TimeSheet");
            //Create Header Row
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex++);
            int columnNumber = 0;
            foreach (var prop in properties)
            {
                if (!report.sortOrder.Contains(prop.Name)) continue; //Only show groupby columns
                row.CreateCell(columnNumber++).SetCellValue(prop.Name);
            }
            //Add Rows
            foreach (var item in summary)
            {
                row = sheet.CreateRow(rowIndex++);
                columnNumber = 0;
                foreach (var prop in properties)
                {
                    if (!report.sortOrder.Contains(prop.Name)) continue;
                    row.CreateCell(columnNumber++).SetCellValue((prop.GetValue(item) ?? "").ToString());
                }
            }
            report.sortOrder.Remove("TotalHours");
            using MemoryStream sw = new();
            workbook.Write(sw, false);
            return sw.ToArray();
        }
        //private byte[] GetPeriodEntrySummary(ActiveUser user, ReportDefinition report)
        //{

        //    var query = GetFilterdQuery(report.Filters).ToList();
        //    //Join User frienly column values
        //    var result = (from t in query
        //                  join p in _sd.Projects on t.ProjectId equals p.Id
        //                  join c in _sd.Clients on p.ClientId equals c.Id
        //                  select new EntryReport()
        //                  {
        //                      User = user.Name,
        //                      Description = t.Description,
        //                      Project = p.Name,
        //                      Client = c.Name,
        //                      StartTime = t.StartTime,
        //                      EndTime = t.EndTime
        //                  }).ToList();

        //    List<EntryReportSummary> summary = new List<EntryReportSummary>();

        //    if (report.sortOrder.Contains("User") && report.sortOrder.Contains("Client") && report.sortOrder.Contains("Project") && report.sortOrder.Contains("Description"))
        //    {
        //        summary = result.OrderBy(r => r.User).ThenBy(r => r.Client).ThenBy(r => r.Project).ThenBy(r => r.Description).GroupBy(x => new { x.User, x.Client, x.Project, x.Description })
        //        .Select(x => new EntryReportSummary
        //        {
        //            User = x.Key.User,
        //            Client = x.Key.Client,
        //            Project = x.Key.Project,
        //            Description = x.Key.Description,
        //            TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
        //        }).ToList();
        //    }
        //    else if (report.sortOrder.Contains("User") && report.sortOrder.Contains("Client") && report.sortOrder.Contains("Project"))
        //    {
        //        summary = result.OrderBy(r => r.User).ThenBy(r => r.Client).ThenBy(r => r.Project).GroupBy(x => new { x.User, x.Client, x.Project })
        //        .Select(x => new EntryReportSummary
        //        {
        //            User = x.Key.User,
        //            Client = x.Key.Client,
        //            Project = x.Key.Project,
        //            TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
        //        }).ToList();
        //    }
        //    else if (report.sortOrder.Contains("User") && report.sortOrder.Contains("Client"))
        //    {
        //        summary = result.OrderBy(r => r.User).ThenBy(r => r.Client).GroupBy(x => new {x.User, x.Client })
        //        .Select(x => new EntryReportSummary
        //        {
        //            User = x.Key.User,
        //            Client = x.Key.Client,
        //            TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
        //        }).ToList();
        //    }
        //    else if (report.sortOrder.Contains("User") )
        //    {
        //        summary = result.OrderBy(r => r.User).GroupBy(x => new { x.User })
        //        .Select(x => new EntryReportSummary
        //        {
        //            User = x.Key.User,
        //            TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
        //        }).ToList();
        //    }
        //    else if (report.sortOrder.Contains("Client") && report.sortOrder.Contains("Project") && report.sortOrder.Contains("Description"))
        //    {
        //        summary = result.OrderBy(r => r.Client).ThenBy(r => r.Project).ThenBy(r => r.Description).GroupBy(x => new { x.Client, x.Project, x.Description })
        //        .Select(x => new EntryReportSummary
        //        {
        //            Client = x.Key.Client,
        //            Project = x.Key.Project,
        //            Description = x.Key.Description,
        //            TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
        //        }).ToList();
        //    }
        //    else if (report.sortOrder.Contains("Client") && report.sortOrder.Contains("Project") )
        //    {
        //        summary = result.OrderBy(r => r.Client).ThenBy(r => r.Project).GroupBy(x => new { x.Client, x.Project })
        //        .Select(x => new EntryReportSummary
        //        {
        //            Client = x.Key.Client,
        //            Project = x.Key.Project,
        //            TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
        //        }).ToList();
        //    }
        //    else if (report.sortOrder.Contains("Client") )
        //    {
        //        summary = result.OrderBy(r => r.Client).GroupBy(x => new { x.Client })
        //        .Select(x => new EntryReportSummary
        //        {
        //            Client = x.Key.Client,
        //            TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
        //        }).ToList();
        //    }
        //    else if (report.sortOrder.Contains("Project") && report.sortOrder.Contains("Description"))
        //    {
        //        summary = result.OrderBy(r => r.Project).ThenBy(r => r.Description).GroupBy(x => new {  x.Project, x.Description })
        //        .Select(x => new EntryReportSummary
        //        {
        //            Project = x.Key.Project,
        //            Description = x.Key.Description,
        //            TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
        //        }).ToList();
        //    }
        //    else if (report.sortOrder.Contains("Project"))
        //    {
        //        summary = result.OrderBy(r => r.Project).GroupBy(x => new { x.Project })
        //        .Select(x => new EntryReportSummary
        //        {
        //            Project = x.Key.Project,
        //            TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
        //        }).ToList();
        //    }
        //    else if (report.sortOrder.Contains("Decription"))
        //    {
        //        summary = result.OrderBy(r => r.Description).GroupBy(x => new { x.Description })
        //        .Select(x => new EntryReportSummary
        //        {
        //            Description = x.Key.Description,
        //            TotalHours = Math.Round(x.Sum(x => (x.EndTime - x.StartTime).TotalHours), 2)
        //        }).ToList();
        //    }

        //    //Greate Xcel Image
        //    report.sortOrder.Add("TotalHours");

        //    PropertyInfo[] properties = typeof(EntryReportSummary).GetProperties();
        //    IWorkbook workbook = new XSSFWorkbook();
        //    var sheet = workbook.CreateSheet("TimeSheet");
        //    //Create Header Row
        //    var rowIndex = 0;
        //    var row = sheet.CreateRow(rowIndex++);
        //    int columnNumber =0;
        //    foreach (var prop in properties)
        //    {
        //        if (!report.sortOrder.Contains(prop.Name)) continue; //Only show groupby columns
        //        row.CreateCell(columnNumber++).SetCellValue(prop.Name);
        //    }
        //    //Add Rows
        //    foreach (var item in summary)
        //    {
        //        row = sheet.CreateRow(rowIndex++);
        //        columnNumber = 0;
        //        foreach (var prop in properties)
        //        {
        //            if (!report.sortOrder.Contains(prop.Name))continue;
        //            row.CreateCell(columnNumber++).SetCellValue((prop.GetValue(item) ?? "").ToString());
        //        }
        //    }
        //    report.sortOrder.Remove("TotalHours");
        //    using (MemoryStream sw = new MemoryStream())
        //    {
        //        workbook.Write(sw, false);
        //        return sw.ToArray();
        //    }
        //}

        private IQueryable<TimeEntry> GetFilterdQuery(List<ReportFilter> filters)
        {
            IQueryable<TimeEntry> query = _context.TimeEntries.AsQueryable().Where(u => !u.Running);
            foreach (var pair in filters)
            {
                switch (pair.Name)
                {
                    case "FromDate":
                        query = query.Where(t => t.StartTime >= pair.Value.ToDate("yyyy-MM-dd"));
                        break;
                    case "ToDate":
                        query = query.Where(t => t.EndTime <= pair.Value.ToDate("yyyy-MM-dd"));
                        break;
                    case "Project":
                        query = query.Where(t => t.ProjectId == (int)pair.Value);
                        break;
                    case "Client":
                        //Todo: test if all the projects for a client is selected.
                        var projectids = _sd.Projects.Where(p => p.ClientId == (int)pair.Value).Select(p => p.Id);
                        //query = query.Where(i => i.ProjectId.IntersectBy(projectids, gr => gr).Any());
                        //query = query.Where(t => t.ProjectId == projects.Select(p => p.Id).Contains(t.ProjectId));
                        //F#%$^#$%^# LINQ filter fails on mongoDb do it one at a time till I figure solution
                        List<TimeEntry> filtered = new();
                        foreach (TimeEntry entry in query)
                        {
                            if (projectids.Contains(entry.ProjectId))
                            {
                                filtered.Add(entry);
                            }
                        }
                        query = filtered.AsQueryable<TimeEntry>();
                        break;
                    case "UserId":
                        query = query.Where(t => t.UserId == (int)pair.Value);
                        break;
                    case "Description":
                        //Todo: test 
                        query = query.Where(t => (t.Description ?? "").Contains((string)pair.Value));
                        break;

                }
            }
            return query;
        }
        //public PagedResult<CsvListItem> GetFiles(string username, int page)
        //{
        //    PagedResult<CsvListItem> result = new PagedResult<CsvListItem>();
        //    string[] files = new string[0];
        //    try
        //    {
        //        files = Directory.GetFiles(reportFolder, $"{username.ToUpper()}*");
        //    }
        //    catch //(Exception ex)
        //    {
        //        throw new Exception($"Application pool does not have access access to the folder: {reportFolder} !");
        //    }

        //    result.count = files.Count();
        //    foreach (string filePath in files)
        //    {
        //        result.result.Add(new CsvListItem(Path.GetFileName(filePath)));
        //    }
        //    //Page the result..
        //    result.result = result.result.OrderByDescending(r => r.ReportDate).Skip((page - 1) * 12).Take(12).ToList();
        //    return result;
        //}

        //public void DeleteReport(string fileName)
        //{
        //    File.Delete($"{reportFolder}\\{fileName}");
        //}

        //public async Task SaveHtmlReport(string data, string fileName)
        //{
        //    string filename = $"{reportFolder}\\{fileName}.html";
        //    await File.WriteAllTextAsync(filename, data);
        //}

        public Dictionary<string, string> MonthList()
        {
            if (_months == null)
            {
                _months = new Dictionary<string, string>();
                //string[] monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames;
                for (int year = DateTime.Now.Year; year >= 2020; year--)
                {
                    for (int month = 1; month <= 12; month++)
                    {
                        //string name = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                        _months.Add($"{month}|{year}", $"{DateTimeFormatInfo.CurrentInfo.GetMonthName(month)} - {year}");
                    }
                }
            }
            return _months;
        }

    }
}
