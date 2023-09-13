using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceLayer.IServices;
using ServiceLayer.Services;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Xml.Linq;

namespace keepdaily_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayController : ControllerBase
    {
        private readonly IDayService _service;
        private readonly FileService _fileService = new("DayImgs");
        private readonly IVideoService _videoService;

        public DayController(IDayService service, IVideoService videoService)
        {
            _service = service;
            _videoService = videoService;
        }

        [HttpGet("Img")]
        public IActionResult GetImage(string name, string type)
        {
            var filePath = _fileService.GetFilePath(name);
            Byte[] b = System.IO.File.ReadAllBytes(filePath);
            return File(b, type);
        }

        [HttpGet]
        public void Test()
        {
            var x = Directory.EnumerateFiles("D:\\InkyProject\\KeepDaily\\keepdaily_be\\keepdaily_be\\bin\\Debug\\net6.0\\DayImgs");
        
        }

        [HttpGet("{planId}/Video")]
        public IActionResult GetVideo(int planId, string start, string end)
        {
            var x = _service.GetDays(planId, start, end).Select(_ => _fileService.GetFilePath(_.ImgName));

            _videoService.ConvertImagesToVideoAsync(x, "test.mp4", 1);
            return Ok();
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
