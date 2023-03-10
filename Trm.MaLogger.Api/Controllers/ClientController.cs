using Microsoft.AspNetCore.Mvc;
using razor.Components.Models;
using Trm.MaLogger.Api.Services;
using Trm.MaLogger.App.Services.DataAccess;
using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _service;

        public ClientController(ClientService service) =>
        _service = service;

        //[HttpGet]
        //public async Task<List<Client>> Get()
        //{
        //    return await _service.GetClientsAsync();
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> Get(int id)
        {
            var Client = await _service.GetClientAsync(id);

            if (Client is null)
            {
                return NotFound();
            }

            return Client;
        }


        [HttpGet]
        public async Task<ActionResult<PagedResult<Client>>> GetPagedClients(int page = 1, int pageSize = 20)
        {
            _ = await _service.GetClientsAsync();
            return _service.GetPagedClientsAsync(page,pageSize);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Client newClient)
        {
            await _service.CreateClientAsync(newClient);

            return CreatedAtAction(nameof(Get), new { id = newClient.Id }, newClient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Client updatedClient)
        {
            var Client = await _service.GetClientAsync(id);

            if (Client is null)
            {
                return NotFound();
            }

            updatedClient.Id = Client.Id;

            await _service.UpdateClientAsync(id, updatedClient);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _service.GetClientAsync(id);

            if (client is null)
            {
                return NotFound();
            }

            await _service.RemoveClientAsync(id);

            return NoContent();
        }
    }
}

