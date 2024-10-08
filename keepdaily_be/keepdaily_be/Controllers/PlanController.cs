﻿using DomainLayer.Models;
using keepdaily_be.Filters;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceLayer.IServices;
using ServiceLayer.Services;
using System.Net;

namespace keepdaily_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _service;
        private readonly IDayService _dayService;
        private readonly IVideoService _videoService;
        private readonly FileService _fileService = new("DayImgs");
        public PlanController(IPlanService service, IDayService dayService, IVideoService videoService)
        {
            _service = service;
            _dayService = dayService;
            _videoService = videoService;
        }

        [HttpGet]
        public IActionResult GetAllPlan(int? uid)
        {
            var plans = _service.GetAllPlan();
            if(uid.HasValue) plans = plans.Where(_ => _.UserId == uid);
            return Ok(plans.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetPlanWithDetail(int id, int? year, int? month)
        {
            var plan = _service.GetPlanWithDetail(id, year, month);
            return plan == null ? NotFound() : Ok(plan);
        }

        [HttpGet("{id}/Video")]
        public async Task<IActionResult> GetPlanVideoAsync(int id, string start, string end)
        {
            var fileName = $"{id}.mp4";
            var videoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Videos", fileName);
            try
            {
                if (System.IO.File.Exists(videoPath)) System.IO.File.Delete(videoPath);

                var day = _dayService.GetDays(id, start, end);
                if (day.Count == 0) throw new InvalidOperationException("No images to export as video.");

                var imgs = day.Select(_ => _fileService.GetFilePath(_.ImgName)).ToList();
                var txts = day.Select(_ => $"{_.Year}/{_.Month}/{_.Date}").ToList();

                await _videoService.ConvertImagesToVideoAsync(imgs, txts, videoPath, 1);

                var memory = new MemoryStream();

                using (var file = new FileStream(videoPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    await file.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, "video/mp4", true);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePlan([FromBody] Plan plan)
        {
            try
            {
                return CreatedAtAction(null, _service.CreatePlan(plan));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdatePlan([FromBody] Plan plan)
        {
            try
            {
                return CreatedAtAction(null, _service.UpdatePlan(plan));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlan(int id)
        {
            try
            {
                _service.DeletePlan(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
