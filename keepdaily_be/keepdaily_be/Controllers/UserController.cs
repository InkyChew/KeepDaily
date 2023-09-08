using AutoMapper;
using DomainLayer.Dto;
using DomainLayer.Models;
using keepdaily_be.Filters;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using Serilog;
using ServiceLayer.IServices;
using ServiceLayer.Services;

namespace keepdaily_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly FileService _fileService = new("UserImgs");
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
                SetTokenCookie(res.RefreshToken);
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
                //var refreshToken = Request.Cookies["refreshToken"];
                //if (refreshToken == null) return BadRequest();
                var res = _service.RefreshToken(user);
                SetTokenCookie(res.RefreshToken);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult GetAllUser(string? name, string? email)
        {
            var users = _service.GetAllUser();
            if (name != null) users = users.Where(_ => _.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) > -1);
            if (email != null) users = users.Where(_ => _.Email == email);
            return Ok(users.ToList());
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

        [HttpGet("Img")]
        public IActionResult GetPhoto(string name, string type)
        {
            var filePath = _fileService.GetFilePath(name);
            Byte[] b = System.IO.File.ReadAllBytes(filePath);
            return File(b, type);
        }

        [HttpPatch("{id}/Img")]
        public async Task<IActionResult> UpdatePhotoAsync(int id, IFormFile file)
        {
            try
            {
                var ext = file.FileName.Split('.')[1];
                var fileName = $"{id}.{ext}";
                await _fileService.Create(file, fileName);
                _service.UpdatePhoto(id, fileName, file.ContentType);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}/Img")]
        public IActionResult DeletePhoto(string name)
        {
            try
            {
                _fileService.Delete(name);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private void SetTokenCookie(string token)
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
