using Microsoft.AspNetCore.Mvc;
using Trm.MaLogger.Api.Services;
using Trm.MaLogger.App.Services.DataAccess;
using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserService _service;

        public UserRoleController(UserService service) =>
        _service = service;

        //[HttpGet]
        //public async Task<List<UserRole>> Get()
        //{
        //    return await _service.GetUserRolesAsync();
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRole>> Get(int id)
        {
            var UserRole = await _service.GetUserRoleAsync(id);

            if (UserRole is null)
            {
                return NotFound();
            }

            return UserRole;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserRole newUserRole)
        {
            await _service.CreateUserRoleAsync(newUserRole);

            return CreatedAtAction(nameof(Get), new { id = newUserRole.Id }, newUserRole);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, UserRole updatedUserRole)
        //{
        //    var UserRole = await _service.GetUserRoleAsync(id);

        //    if (UserRole is null)
        //    {
        //        return NotFound();
        //    }

        //    updatedUserRole.Id = UserRole.Id;

        //    await _service.UpdateUserRoleAsync(id, updatedUserRole);

        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userrole = await _service.GetUserRoleAsync(id);

            if (userrole is null)
            {
                return NotFound();
            }

            await _service.RemoveUserRoleAsync(id);

            return NoContent();
        }
    }
}

