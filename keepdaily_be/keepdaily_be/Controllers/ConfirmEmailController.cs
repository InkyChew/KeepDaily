using Microsoft.AspNetCore.Mvc;
using Serilog;
using ServiceLayer.IServices;

namespace keepdaily_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmEmailController : ControllerBase
    {
        private readonly IConfirmEmailService _service;
        private readonly IUserService _userService;
        public ConfirmEmailController(IConfirmEmailService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetConfirmEmail(int uid, string code)
        {
            var user = _userService.FindUser(uid);
            try
            {
                var isConfirmed = _service.IsEmailConfirm(user.Email, code);
                if (isConfirmed)
                {
                    user.EmailConfirmed = true;
                    _userService.SaveChanges();
                    return Redirect($"http://localhost:4200/email_confirm/1?uid={uid}&email={user.Email}");
                }
                else throw new Exception($"Error confirming email {user.Email}.");
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error(ex.Message);
                return Redirect($"http://localhost:4200/email_confirm/2?uid={uid}&email={user.Email}");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Redirect($"http://localhost:4200/email_confirm/3?uid={uid}&email={user.Email}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendConfirmEmailAsync([FromBody] int uid)
        {
            var user = _userService.FindUser(uid);
            try
            {
                await _service.SendConfirmEmailAsync(user);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("ChangePassword")]
        public IActionResult GetConfirmChangePasswordEmail(int uid, string code)
        {
            var user = _userService.FindUser(uid);
            try
            {
                var isConfirmed = _service.IsEmailConfirm($"CP{user.Email}", code);
                if (isConfirmed)
                {
                    return Redirect($"http://localhost:4200/forgot_password/1?uid={uid}&email={user.Email}");
                }
                else throw new Exception($"Error confirming email {user.Email}.");
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error(ex.Message);
                return Redirect($"http://localhost:4200/forgot_password/2?uid={uid}&email={user.Email}");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Redirect($"http://localhost:4200/forgot_password/3?uid={uid}&email={user.Email}");
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> SendConfirmChangePasswordEmailAsync([FromForm] string email)
        {
            try
            {
                var user = _userService.GetUser(email);
                if (user == null) return NotFound(email);
                await _service.SendConfirmChangePasswordEmailAsync(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
