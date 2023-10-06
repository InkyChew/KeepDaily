using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServiceLayer.IServices;

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

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            _service.DeleteMessage(id);
            return NoContent();
        }
    }
}
