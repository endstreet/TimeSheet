using razor.Components.Models;
using Trm.MaLogger.MsData.Models;
using Trm.MaLogger.MsData.Views;

namespace Trm.MaLogger.App.Services.DataAccess
{
    public interface ITimeEntrieservice
    {
        Task ContinueFromEntryAsync(EntryView timeEntry);
        Task<EntryView> CreateTimeEntryAsync(EntryView displayEntry);
        Task<TimeEntry> CreateTimeEntryAsync(TimeEntry newTimeEntry);
        TimeEntry FromDisplayEntry(EntryView entry);
        EntryView FromEntry(TimeEntry entry);
        Task<PagedResult<EntryView>> GetPagedTimeEntriesAsync(int userid, int page = 1, int pageSize = 16);
        Task<EntryView> GetRunningEntryDisplayAsync(int userid);
        Task<TimeEntry?> GetRunningTimeEntriesAsync(int userid);
        Task<TimeEntry> GetTimeEntryAsync(int Id);
        Task<List<TimeEntry>> GetTimeEntriesAsync();
        Task RemoveTimeEntryAsync(int Id);
        Task<EntryView> UpdateTimeEntryAsync(int Id, EntryView displayEntry);
        Task UpdateTimeEntryAsync(int Id, TimeEntry updatedTimeEntry);
    }
}