using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceLayer.IServices;
using System.Net;

namespace keepdaily_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _service;

        public FriendController(IFriendService service)
        {
            _service = service;
        }

        [HttpGet("{uid}")]
        public IActionResult GetUserFriends(int uid)
        {
            return Ok(_service.GetUserFriends(uid).ToList());
        }

        [HttpGet]
        public IActionResult GetFriend(int uid, int fid)
        {
            var friend = _service.GetFriend(uid, fid);
            return friend == null ? NotFound() : Ok(friend);
        }

        [HttpPost]
        public IActionResult AddFriend(Friend friend)
        {
            try
            {
                _service.AddFriend(friend);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        public IActionResult DeleteFriend(Friend friend)
        {
            try
            {
                _service.DeleteFriend(friend);
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
