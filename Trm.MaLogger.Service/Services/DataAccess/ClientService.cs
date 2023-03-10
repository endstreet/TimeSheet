using Microsoft.EntityFrameworkCore;
using razor.Components.Models;
using Trm.MaLogger.MsData.Context;
using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.App.Services.DataAccess
{
    public class ClientService : IClientService
    {
        public ClientService(MaLoggerDbContext context, StaticDataService sd)
        {
            _context = context;
            _sd = sd;
        }

        readonly MaLoggerDbContext _context;
        readonly StaticDataService _sd;
        #region clients
        public async Task<List<Client>> GetClientsAsync()
        {
            _sd.Clients ??= await _context.Clients.ToListAsync();
            return _sd.Clients;
        }

        public PagedResult<Client> GetPagedClientsAsync(int page = 1,int pageSize = 20)
        {
            PagedResult<Client> clients = new()
            {
                count = _sd.Clients.Count,
                Result = _sd.Clients.Skip(page - 1).Take(pageSize).ToList()
            };

            return clients;
        }
        public async Task<Client?> GetClientAsync(int Id)
        {
            return (await GetClientsAsync()).FirstOrDefault(x => x.Id == Id);
        }

        public async Task CreateClientAsync(Client newClient)
        {
            _context.Clients.Add(newClient);
            await _context.SaveChangesAsync();
            _sd.Clients = await _context.Clients.ToListAsync();
        }
        public async Task UpdateClientAsync(int Id, Client updatedClient)
        {
            _context.Clients.Update(updatedClient);
            _sd.Clients = await _context.Clients.ToListAsync();
        }

        public async Task RemoveClientAsync(int Id)
        {
            var client = await _context.Clients.Where(x => x.Id == Id).FirstAsync();
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            _sd.Clients = await _context.Clients.ToListAsync();
        }
        #endregion
    }
}
