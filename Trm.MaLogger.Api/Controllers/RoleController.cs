using Microsoft.AspNetCore.Mvc;
using Trm.MaLogger.App.Services.DataAccess;
using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserService _service;

        public RoleController(UserService service) =>
        _service = service;

        [HttpGet]
        public async Task<List<Role>> Get()
        {
            return await _service.GetRolesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> Get(int id)
        {
            var Role = await _service.GetRoleAsync(id);

            if (Role is null)
            {
                return NotFound();
            }

            return Role;
        }

        //[HttpPost]
        //public async Task<IActionResult> Post(Role newRole)
        //{
        //    await _service.CreateRoleAsync(newRole);

        //    return CreatedAtAction(nameof(Get), new { id = newRole.Id }, newRole);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, Role updatedRole)
        //{
        //    var Role = await _service.GetRoleAsync(id);

        //    if (Role is null)
        //    {
        //        return NotFound();
        //    }

        //    updatedRole.Id = Role.Id;

        //    await _service.UpdateRoleAsync(id, updatedRole);

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var role = await _service.GetRoleAsync(id);

        //    if (role is null)
        //    {
        //        return NotFound();
        //    }

        //    await _service.RemoveRoleAsync(id);

        //    return NoContent();
        //}
    }
}

