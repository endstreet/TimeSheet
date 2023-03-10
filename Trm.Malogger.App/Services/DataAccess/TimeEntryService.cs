using Microsoft.EntityFrameworkCore;
using razor.Components.Models;
using Trm.MaLogger.MsData.Context;
using Trm.MaLogger.MsData.Models;
using Trm.MaLogger.MsData.Views;

namespace Trm.MaLogger.App.Services.DataAccess
{
    public class TimeEntrieservice : ITimeEntrieservice
    {
        readonly MaLoggerDbContext _context;
        readonly StaticDataService _sd;
        public TimeEntrieservice(MaLoggerDbContext context, StaticDataService sd)
        {
            _context = context;
            _sd = sd;
        }

        #region timeentry
        public async Task<List<TimeEntry>> GetTimeEntriesAsync()
        {
            return await _context.TimeEntries.ToListAsync();
        }

        public async Task<TimeEntry?> GetRunningTimeEntriesAsync(int userid)
        {
            var result = await _context.TimeEntries.Where(u => u.Running && u.UserId == userid).ToListAsync();
            if (result.Any()) return result.First();
            return null;
        }

        public async Task<EntryView> GetRunningEntryDisplayAsync(int userid)
        {
            var result = await _context.TimeEntries.Where(u => u.Running && u.UserId == userid).ToListAsync();
            if (result.Any())
            {
                TimeEntry entry = result.First();
                return FromEntry(entry);
            }
            return new EntryView(userid);
        }

        public async Task<PagedResult<EntryView>> GetPagedTimeEntriesAsync(int userid, int page = 1, int pageSize = 16)
        {
            PagedResult<EntryView> result = new();
            var _entries = await _context.TimeEntries.Where(u => !u.Running && u.UserId == userid).OrderByDescending(t => t.StartTime).Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
            result.count = await _context.TimeEntries.Where(u => !u.Running && u.UserId == userid).CountAsync();
            result.Result = (from t in _entries
                             join p in _sd.Projects on t.ProjectId equals p.Id
                             join c in _sd.Clients on p.ClientId equals c.Id
                             select new EntryView(userid)
                             {
                                 Id = t.Id,
                                 Description = t.Description,
                                 ActiveProject = p,
                                 ProjectId = t.ProjectId,
                                 ClientId = c.Id,
                                 Client = c.Name,
                                 StartTime = t.StartTime,
                                 EndTime = t.EndTime,
                                 Running = t.Running
                             }).ToList();

            return result;
            //return await _context.TimeEntries.Where(u => !u.Running && u.UserId == userid).ToListAsync();
        }

        public async Task<TimeEntry> GetTimeEntryAsync(int Id)
        {
            return await _context.TimeEntries.Where(x => x.Id == Id).FirstAsync();
        }
        public async Task ContinueFromEntryAsync(EntryView timeEntry)
        {
            TimeEntry continueEntry = FromDisplayEntry(timeEntry);
            //create a new time entry based on this one
            continueEntry.Id = 0; //Maybe -1
            continueEntry.StartTime = DateTime.Now;
            continueEntry.EndTime = DateTimeOffset.MinValue;
            continueEntry.Running = true;
            _ = await CreateTimeEntryAsync(continueEntry);
        }

        //Todo:use mapper
        public EntryView FromEntry(TimeEntry entry)
        {
            return new EntryView()
            {
                Id = entry.Id,
                Description = entry.Description,
                ProjectId = entry.ProjectId,
                ClientId = entry.ProjectId > 0 ? _sd.Projects.First(p => p.Id == entry.ProjectId).ClientId : 0,
                UserId = entry.UserId,
                StartTime = entry.StartTime,
                EndTime = entry.EndTime,
                Running = entry.Running
            };
        }
        //Todo:use mapper
        public TimeEntry FromDisplayEntry(EntryView entry)
        {
            return new TimeEntry()
            {
                Id = entry.Id,
                Description = entry.Description,
                ProjectId = entry.ActiveProject.Id,
                UserId = entry.UserId,
                StartTime = entry.StartTime,
                EndTime = entry.EndTime,
                Running = entry.Running,
            };
        }
        public async Task<TimeEntry> CreateTimeEntryAsync(TimeEntry newTimeEntry)
        {
            _context.TimeEntries.Add(newTimeEntry);
            await _context.SaveChangesAsync();
            return newTimeEntry;
        }
        public async Task<EntryView> CreateTimeEntryAsync(EntryView displayEntry)
        {
            TimeEntry newTimeEntry = FromDisplayEntry(displayEntry);
            _context.TimeEntries.Add(newTimeEntry);
            await _context.SaveChangesAsync(true);
            return FromEntry(newTimeEntry);
        }
        public async Task UpdateTimeEntryAsync(int Id, TimeEntry updatedTimeEntry)
        {
            _context.TimeEntries.Update(updatedTimeEntry);
            await _context.SaveChangesAsync(true);
        }
        public async Task<EntryView> UpdateTimeEntryAsync(int Id, EntryView displayEntry)
        {
            var entry = FromDisplayEntry(displayEntry);
            TimeEntry dentry = await _context.TimeEntries.Where(e => e.Id == Id).FirstAsync();
            dentry.Description = entry.Description;
            dentry.ProjectId = entry.ProjectId;
            dentry.UserId = entry.UserId;
            dentry.StartTime = entry.StartTime;
            dentry.EndTime = entry.EndTime;
            dentry.Running = entry.Running;

            _context.TimeEntries.Update(dentry);
            await _context.SaveChangesAsync(true);
            return FromEntry(await _context.TimeEntries.Where(x => x.Id == Id).FirstAsync());
        }
        public async Task RemoveTimeEntryAsync(int Id)
        {
            var timeEntry = await _context.TimeEntries.Where(x => x.Id == Id).FirstAsync();
            _context.TimeEntries.Remove(timeEntry);
            await _context.SaveChangesAsync();
        }
        #endregion
    }

}
