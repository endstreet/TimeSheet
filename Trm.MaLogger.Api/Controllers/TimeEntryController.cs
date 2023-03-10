using Microsoft.AspNetCore.Mvc;
using razor.Components.Models;
using Trm.MaLogger.App.Services.DataAccess;
using Trm.MaLogger.MsData.Models;
using Trm.MaLogger.MsData.Views;

namespace Trm.MaLogger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeEntryController : ControllerBase
    {
        private readonly TimeEntryService _service;
        public TimeEntryController(TimeEntryService service) => _service = service;

        [HttpGet]
        public async Task<List<TimeEntry>> Get()
        {
            return await _service.GetTimeEntriesAsync();
        }

        [HttpGet("paged/{userid}")]
        public async Task<PagedResult<EntryView>> GetPaged(int userid,int page =1,int pageSize=16)
        {
            return await _service.GetPagedTimeEntriesAsync(userid,page,pageSize);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeEntry>> Get(int id)
        {
            var TimeEntry = await _service.GetTimeEntryAsync(id);

            if (TimeEntry is null)
            {
                return NotFound();
            }

            return TimeEntry;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TimeEntry newTimeEntry)
        {
            await _service.CreateTimeEntryAsync(newTimeEntry);

            return CreatedAtAction(nameof(Get), new { id = newTimeEntry.Id }, newTimeEntry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TimeEntry updatedTimeEntry)
        {
            var TimeEntry = await _service.GetTimeEntryAsync(id);

            if (TimeEntry is null)
            {
                return NotFound();
            }

            updatedTimeEntry.Id = TimeEntry.Id;

            await _service.UpdateTimeEntryAsync(id, updatedTimeEntry);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var timeentry = await _service.GetTimeEntryAsync(id);

            if (timeentry is null)
            {
                return NotFound();
            }

            await _service.RemoveTimeEntryAsync(id);

            return NoContent();
        }
    }
}

