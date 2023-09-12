using DomainLayer.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using Serilog;
using ServiceLayer.IServices;

namespace keepdaily_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthGoogleController : Controller
    {
        private readonly IUserService _userService;

        public OAuthGoogleController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult ValidateGoogleId([FromForm] AuthGoogle auth)
        {
            try
            {
                var cookieToken = Request.Cookies["g_csrf_token"];
                if (cookieToken != auth.Token)
                {
                    throw new BadRequestException("Failed to verify double submit cookie.");
                }

                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new List<string>() { "272976394573-m3k7tm2m60a4dkm6qj8mlqn79dh48ckm.apps.googleusercontent.com" }
                };
                var payload = GoogleJsonWebSignature.ValidateAsync(auth.Credential, settings).Result;

                var user = _userService.GetUser(payload.Email);
                AuthenticateUser? res;
                if (user != null)
                {
                    res = _userService.LoginWithGoogle(user);
                    SetTokenCookie(res.RefreshToken);
                }
                else
                {
                    User authUser = new()
                    {
                        Name = payload.Name,
                        Email = payload.Email,
                        ImgName = payload.Picture,
                        ImgType = "Google",
                        EmailConfirmed = payload.EmailVerified,
                        IsActive = true
                    };
                    res = _userService.RegisterWithGoogle(authUser);
                    SetTokenCookie(res.RefreshToken);
                }

                return res == null ? throw new BadRequestException("User is null.") : View("OAuth", res);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                ViewBag.err = true;
                return View("OAuth");
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

        [HttpPost("Test")]
        public IActionResult Test()
        {
            return View("OAuth");
        }
    }
}
