using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.App.Services
{
    public interface IStaticDataService
    {
        List<Client> Clients { get; set; }
        List<Project> Projects { get; set; }
    }
}