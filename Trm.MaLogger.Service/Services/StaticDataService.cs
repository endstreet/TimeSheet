using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.App.Services
{
    public class StaticDataService : IStaticDataService
    {
        public List<Project> Projects { get; set; } = null!;
        public List<Client> Clients { get; set; } = null!;

    }
}
