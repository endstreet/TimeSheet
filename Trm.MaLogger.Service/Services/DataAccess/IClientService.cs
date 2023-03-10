using razor.Components.Models;
using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.App.Services.DataAccess
{
    public interface IClientService
    {
        Task CreateClientAsync(Client newClient);
        Task<Client?> GetClientAsync(int Id);
        Task<List<Client>> GetClientsAsync();
        PagedResult<Client> GetPagedClientsAsync(int page = 1,int pageSize = 20);
        Task RemoveClientAsync(int Id);
        Task UpdateClientAsync(int Id, Client updatedClient);
    }
}