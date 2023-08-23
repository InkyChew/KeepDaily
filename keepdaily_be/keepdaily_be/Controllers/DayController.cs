using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceLayer.IServices;
using ServiceLayer.Services;
using System.Net;
using System.Text.Json;

namespace keepdaily_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayController : ControllerBase
    {
        private readonly IDayService _service;
        private readonly FileService _fileService = new("DayImgs");

        public DayController(IDayService service)
        {
            _service = service;
        }

        [HttpGet("Img")]
        public IActionResult Get(string name, string type)
        {
            var filePath = _fileService.GetFilePath(name);
            Byte[] b = System.IO.File.ReadAllBytes(filePath);
            return File(b, type);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDayAsync([FromForm] string data, IFormFile file)
        {
            try
            {
                var day = JsonSerializer.Deserialize<Day>(data, new JsonSerializerOptions {PropertyNameCaseInsensitive = true}) ?? throw new Exception("Json Day parse error.");
                await _fileService.Create(file, day.ImgName);
                return CreatedAtAction(null, _service.UpdateDay(day));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDayAsync([FromForm] string data, IFormFile file)
        {
            try
            {
                var day = JsonSerializer.Deserialize<Day>(data) ?? throw new Exception("Json Day parse error.");
                await _fileService.Create(file, day.ImgName);
                _service.UpdateDay(day);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDay(int id)
        {
            try
            {
                var day = _service.DeleteDay(id);
                _fileService.Delete(day.ImgName);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
