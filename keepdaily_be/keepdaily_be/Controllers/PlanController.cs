using DomainLayer.Dto;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceLayer.IServices;

namespace keepdaily_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _service;

        public PlanController(IPlanService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllPlan()
        {
            return Ok(_service.GetAllPlan());
        }

        [HttpGet("{id}")]
        public IActionResult GetPlanWithDetail(int id, int? year, int? month)
        {
            var plan = _service.GetPlanWithDetail(id, year, month);
            return plan == null ? NotFound() : Ok(plan);
        }

        [HttpPost]
        public IActionResult CreatePlan([FromBody] Plan plan)
        {
            try
            {
                return CreatedAtRoute(null, _service.CreatePlan(plan));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult UpdatePlan([FromBody] Plan plan)
        {
            try
            {
                _service.UpdatePlan(plan);
                return NoContent();
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
