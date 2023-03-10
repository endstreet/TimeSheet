using Microsoft.AspNetCore.Mvc;
using Trm.MaLogger.Api.Services;
using Trm.MaLogger.App.Services.DataAccess;
using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service) =>
        _service = service;

        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await _service.GetUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _service.GetUserAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User newUser)
        {
            await _service.CreateUserAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User updatedUser)
        {
            var user = await _service.GetUserAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            updatedUser.Id = user.Id;

            await _service.UpdateUserAsync(id, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _service.GetUserAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _service.RemoveUserAsync(id);

            return NoContent();
        }
    }
}

