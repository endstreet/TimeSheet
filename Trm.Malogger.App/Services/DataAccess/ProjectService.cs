using Microsoft.EntityFrameworkCore;
using razor.Components.Models;
using Trm.MaLogger.MsData.Context;
using Trm.MaLogger.MsData.Models;
using Trm.MaLogger.MsData.Views;

namespace Trm.MaLogger.App.Services.DataAccess
{
    public class ProjectService : IProjectService
    {
        public ProjectService(MaLoggerDbContext context, StaticDataService sd)
        {
            _context = context;
            _sd = sd;
        }
        readonly MaLoggerDbContext _context;
        readonly StaticDataService _sd;

        #region projects
        public async Task<List<Project>> GetProjectsAsync()
        {
            _sd.Projects ??= await _context.Projects.ToListAsync();
            return _sd.Projects;
        }
        public Project? GetProjectAsync(int Id)
        {
            return _sd.Projects.Where(x => x.Id == Id).FirstOrDefault();
        }

        public async Task<Project> CreateProjectAsync(Project newProject)
        {
            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();
            _sd.Projects = await _context.Projects.Where(_ => true).ToListAsync();
            return newProject;
        }
        public async Task UpdateProjectAsync(int Id, Project updatedProject)
        {
            _context.Projects.Update(updatedProject);
            await _context.SaveChangesAsync();
            _sd.Projects = await _context.Projects.ToListAsync();
        }

        public async Task RemoveProjectAsync(int Id)
        {
            var project = await _context.Projects.Where(x => x.Id == Id).FirstAsync();
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
        #endregion

        /// <summary>
        /// Gets PieData for User For current Month
        /// </summary>
        /// <returns></returns>
        public async Task<PieData> GetUserPieData(int userId)
        {
            PieData pd = new()
            {
                ChartName = "Project User activity"
            };
            double total = _context.TimeEntries.Where(t => t.UserId == userId && t.StartTime > DateTime.Now.AddMonths(-1)).Count();
            pd.TotalItems = (int)total;
            List<TimeEntry> entries = await _context.TimeEntries.Where(x => x.UserId == userId && x.StartTime > DateTime.Now.AddMonths(-1) && x.ProjectId > 0).ToListAsync();
            List<RawSegment> query = entries
                .GroupBy(c => c.ProjectId)
                .Select(o => new RawSegment
                {
                    Name = _sd.Projects.First(p => p.Id == o.Key).Name,
                    Count = o.Where(c => c.ProjectId == o.Key).Count(),
                    Color = _sd.Projects.First(p => p.Id == o.Key).Color
                }).ToList();



            foreach (var rs in query)
            {
                PieSegment ps = new()
                {
                    Name = rs.Name,
                    Percent = rs.Count / (total / 100),
                    Color = rs.Color
                };
                pd.Segments.Add(ps);
            }

            return pd;
        }

        /// <summary>
        /// Gets PieData for User For current Month
        /// </summary>
        /// <returns></returns>
        public async Task<PieData> GetCoPieData()
        {
            PieData pd = new()
            {
                ChartName = "Project Company activity"
            };
            double total = _context.TimeEntries.Where(t => t.StartTime > DateTime.Now.AddMonths(-1)).Count();
            //Todo: throw exception if no records
            pd.TotalItems = (int)total;
            List<TimeEntry> entries = await _context.TimeEntries.Where(x => x.StartTime > DateTime.Now.AddMonths(-1) && x.ProjectId > 0).ToListAsync();
            List<RawSegment> query = entries
                .GroupBy(c => c.ProjectId)
                .Select(o => new RawSegment
                {
                    Name = _sd.Projects.First(p => p.Id == o.Key).Name,
                    Count = o.Where(c => c.ProjectId == o.Key).Count(),
                    Color = _sd.Projects.First(p => p.Id == o.Key).Color
                }).ToList();

            foreach (var rs in query)
            {
                PieSegment ps = new()
                {
                    Name = rs.Name,
                    Percent = (rs.Count > 0 ? rs.Count / (total / 100) : 0),
                    Color = rs.Color
                };
                pd.Segments.Add(ps);
            }

            return pd;
        }

        /// <summary>
        /// Gets PieData for User For current Month
        /// </summary>
        /// <returns></returns>
        public async Task<PieData> GetClientUserPieData(int userId)
        {
            PieData pd = new()
            {
                ChartName = "Client User activity"
            };
            double total = _context.TimeEntries.Where(t => t.UserId == userId && t.StartTime > DateTime.Now.AddMonths(-1) && t.ProjectId > 0).Count();
            pd.TotalItems = (int)total;
            List<EntryView> entries = await GetTimeEntriesAsync(userId);
            List<RawSegment> query = entries
                .GroupBy(c => c.ClientId)
                .Select(o => new RawSegment
                {
                    Name = _sd.Clients.First(p => p.Id == o.Key).Name,
                    Count = o.Where(c => c.ClientId == o.Key).Count(),
                    Color = _sd.Projects.First(p => p.ClientId == o.Key).Color
                }).ToList();



            foreach (var rs in query)
            {
                PieSegment ps = new()
                {
                    Name = rs.Name,
                    Percent = rs.Count / (total / 100),
                    Color = rs.Color
                };
                pd.Segments.Add(ps);
            }

            return pd;
        }

        /// <summary>
        /// Gets PieData for User For current Month
        /// </summary>
        /// <returns></returns>
        public async Task<PieData> GetClientCoPieData()
        {
            PieData pd = new()
            {
                ChartName = "Client Company activity"
            };
            double total = _context.TimeEntries.Where(t => t.StartTime > DateTime.Now.AddMonths(-1) && t.ProjectId > 0).Count();
            //Todo: throw exception if no records
            pd.TotalItems = (int)total;
            List<EntryView> entries = await GetTimeEntriesAsync();
            List<RawSegment> query = entries
                .GroupBy(c => c.ClientId)
                .Select(o => new RawSegment
                {
                    Name = _sd.Clients.First(p => p.Id == o.Key).Name,
                    Count = o.Where(c => c.ClientId == o.Key).Count(),
                    Color = _sd.Projects.First(p => p.ClientId == o.Key).Color
                }).ToList();

            foreach (var rs in query)
            {
                PieSegment ps = new()
                {
                    Name = rs.Name,
                    Percent = (rs.Count > 0 ? rs.Count / (total / 100) : 0),
                    Color = rs.Color
                };
                pd.Segments.Add(ps);
            }

            return pd;
        }

        public async Task<List<EntryView>> GetTimeEntriesAsync(int userId)
        {
            var _entries = await _context.TimeEntries.Where(x => x.UserId == userId && x.StartTime > DateTime.Now.AddMonths(-1)).ToListAsync();
            var result = (from t in _entries
                          join p in _sd.Projects on t.ProjectId equals p.Id
                          select new EntryView(userId)
                          {
                              Id = t.Id,
                              Description = t.Description,
                              ProjectId = p.Id,
                              ClientId = p.ClientId,
                              StartTime = t.StartTime,
                              EndTime = t.EndTime,
                              Running = t.Running
                          }).ToList();

            return result;
            //return await _context.TimeEntries.Where(u => !u.Running && u.UserId == userid).ToListAsync();
        }

        public async Task<List<EntryView>> GetTimeEntriesAsync()
        {
            var _entries = await _context.TimeEntries.Where(x => x.StartTime > DateTime.Now.AddMonths(-1)).ToListAsync();
            var result = (from t in _entries
                          join p in _sd.Projects on t.ProjectId equals p.Id
                          select new EntryView()
                          {
                              Id = t.Id,
                              Description = t.Description,
                              ProjectId = p.Id,
                              ClientId = p.ClientId,
                              StartTime = t.StartTime,
                              EndTime = t.EndTime,
                              Running = t.Running
                          }).ToList();

            return result;
            //return await _context.TimeEntries.Where(u => !u.Running && u.UserId == userid).ToListAsync();
        }
    }


}
