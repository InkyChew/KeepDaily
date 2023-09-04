using AutoMapper;
using DomainLayer.Dto;
using DomainLayer.Models;
using keepdaily_be.Filters;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;
        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
                var res = _service.Login(email, password);
                setTokenCookie(res.RefreshToken);
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

        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken([FromBody] UserDto userdto)
        {
            try
            {
                var user = _mapper.Map<User>(userdto);
                var refreshToken = Request.Cookies["refreshToken"];
                if (refreshToken == null) return BadRequest();
                var res = _service.RefreshToken(user);
                setTokenCookie(res.RefreshToken);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _service.GetUser(id);
            return user == null ? NotFound() : Ok(user);
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserDto userdto)
        {
            try
            {
                var user = _mapper.Map<User>(userdto);
                return CreatedAtAction(null, _service.UpdateUserInfo(user));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePassword(int id, [FromForm] string password)
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

        private void setTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite = SameSiteMode.None,
                Secure = true,
                Domain = "localhost",
                IsEssential = true,
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
