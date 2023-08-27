using DomainLayer.Dto;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Caching.Memory;
using SendGrid.Helpers.Errors.Model;
using Serilog;
using ServiceLayer.IServices;

namespace keepdaily_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _cache;
        public UserController(IUserService service, IEmailService emailService, IMemoryCache cache)
        {
            _service = service;
            _emailService = emailService;
            _cache = cache;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromForm] string name, [FromForm] string email, [FromForm] string password)
        {
            try
            {
                var user = await _service.RegisterAsync(name, email, password);
                var res = new User { Id = user.Id, Name = user.Name, Email = user.Email };
                return CreatedAtAction(null, res);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromForm] string email, [FromForm] string password)
        {
            try
            {
                var user = _service.Login(email, password);
                var res = new User { Id = user.Id, Name = user.Name, Email = user.Email };
                return CreatedAtAction(null, res);
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(new { id = ex.Message });
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _service.GetUser(id);
            return user == null ? NotFound() : Ok(new VUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Description = user.Description,
                ImgName = user.ImgName,
                ImgType = user.ImgType,
                LineAccessToken = user.LineAccessToken,
                LineNotify = user.LineNotify,
                EmailNotify = user.EmailNotify,
            });
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] VUser user)
        {
            try
            {
                return CreatedAtAction(null, _service.UpdateUserInfo(user));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePassword(int id, [FromBody] string password)
        {
            try
            {
                _service.UpdatePassword(id, password);
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
