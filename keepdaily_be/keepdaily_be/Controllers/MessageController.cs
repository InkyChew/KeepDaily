using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using ServiceLayer.IServices;
using System.Net;

namespace keepdaily_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllMessage(int uid)
        {
            return Ok(_service.GetAllUserMessage(uid));
        }

        [HttpPatch]
        public IActionResult UpdateReadMessage([FromBody] List<Message> messages)
        {
            try
            {
                _service.UpdateReadMessage(messages);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            try
            {
                _service.DeleteMessage(id);
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
