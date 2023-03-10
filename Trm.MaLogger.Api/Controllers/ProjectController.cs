using Microsoft.AspNetCore.Mvc;
using Trm.MaLogger.App.Services.DataAccess;
using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _service;

        public ProjectController(ProjectService service) => _service = service;

        [HttpGet]
        public async Task<List<Project>> Get()
        {
            return await _service.GetProjectsAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<Project> Get(int id)
        {
            var Project = _service.GetProjectAsync(id);

            if (Project is null)
            {
                return NotFound();
            }

            return Project;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Project newProject)
        {
            await _service.CreateProjectAsync(newProject);

            return CreatedAtAction(nameof(Get), new { id = newProject.Id }, newProject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Project updatedProject)
        {
            var Project = _service.GetProjectAsync(id);

            if (Project is null)
            {
                return NotFound();
            }

            updatedProject.Id = Project.Id;

            await _service.UpdateProjectAsync(id, updatedProject);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = _service.GetProjectAsync(id);

            if (project is null)
            {
                return NotFound();
            }

            await _service.RemoveProjectAsync(id);

            return NoContent();
        }
    }
}

