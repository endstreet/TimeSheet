using razor.Components.Models;
using Trm.MaLogger.MsData.Models;
using Trm.MaLogger.MsData.Views;

namespace Trm.MaLogger.App.Services.DataAccess
{
    public interface IProjectService
    {
        Task<Project> CreateProjectAsync(Project newProject);
        Task<PieData> GetClientCoPieData();
        Task<PieData> GetClientUserPieData(int userId);
        Task<PieData> GetCoPieData();
        Project? GetProjectAsync(int Id);
        Task<List<Project>> GetProjectsAsync();
        Task<List<EntryView>> GetTimeEntriesAsync();
        Task<List<EntryView>> GetTimeEntriesAsync(int userId);
        Task<PieData> GetUserPieData(int userId);
        Task RemoveProjectAsync(int Id);
        Task UpdateProjectAsync(int Id, Project updatedProject);
    }
}