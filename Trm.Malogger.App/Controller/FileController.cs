using Microsoft.AspNetCore.Mvc;
using Trm.MaLogger.App.Services;
using Trm.MaLogger.MsData.Models;

namespace Trm.MaLogger.App.Controller
{
    [Route("[controller]")]
    //[ApiController]
    public class FileController : ControllerBase
    {
        readonly ReportDataService _service;

        public FileController(ReportDataService service)
        {

            _service = service;
        }

        /// <summary>
        /// Return a locally stored file based on id to the requesting client
        /// </summary>
        /// <param name="id">unique identifier for the requested file</param>
        /// <returns>an IAction Result</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Download(string id)
        {

            // Get all bytes of the file and return the file with the specified file contents 

            Report report = await _service.GetReport(id);
            return File(report.Content ?? Array.Empty<byte>(), "application/octet-stream", report.ReportName);
            //string path = Path.Combine(_reportFolder, id);

            //if (System.IO.File.Exists(path))
            //{
            //    // Get all bytes of the file and return the file with the specified file contents 
            //    byte[] b = await System.IO.File.ReadAllBytesAsync(path);
            //    return File(b, "application/octet-stream");
            //}
            //else
            //{
            //    // return error if file not found
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

        }
    }
}
