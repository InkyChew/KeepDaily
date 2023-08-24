using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceLayer.IServices;
using System.Net;

namespace keepdaily_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineNotifyController : ControllerBase
    {
        private readonly ILineNotifyService _service;
        private readonly IUserService _userService;

        public LineNotifyController(ILineNotifyService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult RedirectToExternalLink(string email)
        {
            return Content(_service.GetAuthorizationUrl(email));
        }

        [HttpPost]
        public async Task<IActionResult> PostTokenAsync([FromForm] LineAuthRes authRes)
        {
            try
            {
                if(authRes.Code != null)
                {
                    var token = await _service.PostTokenAsync(authRes.Code);
                    _userService.UpdateUserLineToken(authRes.State, token);
                    return Redirect("http://localhost:4200/plan/1");
                }
                Console.WriteLine(authRes.Error_description);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Some error occurs from Line.");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
