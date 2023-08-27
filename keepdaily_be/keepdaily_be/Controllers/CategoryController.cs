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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            return Ok(_service.GetAllCategory());
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category ctg)
        {
            try
            {
                return CreatedAtAction(null, _service.CreateCategory(ctg));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult UpdateCategory([FromBody] Category ctg)
        {
            try
            {
                return CreatedAtAction(null, _service.UpdateCategory(ctg));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _service.DeleteCategory(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
