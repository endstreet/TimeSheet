using Microsoft.AspNetCore.Mvc;
using Trm.MaLogger.Api.Services;
using Trm.MaLogger.App.Services.DataAccess;
using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProjectController : ControllerBase
    {
        private readonly UserService _service;

        public UserProjectController(UserService service) =>
        _service = service;

        [HttpGet]
        public async Task<List<UserProject>> Get()
        {
            return await _service.GetUserProjectsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProject>> Get(int id)
        {
            var UserProject = await _service.GetUserProjectAsync(id);

            if (UserProject is null)
            {
                return NotFound();
            }

            return UserProject;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserProject newUserProject)
        {
            await _service.CreateUserProjectAsync(newUserProject);

            return CreatedAtAction(nameof(Get), new { id = newUserProject.Id }, newUserProject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserProject updatedUserProject)
        {
            var UserProject = await _service.GetUserProjectAsync(id);

            if (UserProject is null)
            {
                return NotFound();
            }

            updatedUserProject.Id = UserProject.Id;

            await _service.UpdateUserProjectAsync(id, updatedUserProject);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userproject = await _service.GetUserProjectAsync(id);

            if (userproject is null)
            {
                return NotFound();
            }

            await _service.RemoveUserProjectAsync(id);

            return NoContent();
        }
    }
}

