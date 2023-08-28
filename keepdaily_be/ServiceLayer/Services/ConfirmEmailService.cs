using DomainLayer.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using ServiceLayer.IServices;
using System.Text;
using System.Text.Encodings.Web;

namespace ServiceLayer.Services
{
    public class ConfirmEmailService : IConfirmEmailService
    {
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _cache;

        public ConfirmEmailService(IEmailService emailService, IMemoryCache cache)
        {
            _emailService = emailService;
            _cache = cache;
        }

        public async Task SendConfirmEmailAsync(User user)
        {
            var code = GenerateCode();
            _cache.Set(user.Email, code, TimeSpan.FromDays(1));
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = $"https://localhost:5000/api/ConfirmEmail?uid={user.Id}&code={code}";
            await _emailService.SendEmailAsync(user.Email, "Confirm your email",
                        $"Please confirm your account in a day by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        }

        public async Task SendConfirmChangePasswordEmailAsync(User user)
        {
            var code = GenerateCode();
            _cache.Set($"CP{user.Email}", code, TimeSpan.FromMinutes(10));
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = $"https://localhost:5000/api/ConfirmEmail/ChangePassword?uid={user.Id}&code={code}";
            await _emailService.SendEmailAsync(user.Email, "Change Password",
                        $"Change your password in 10 minutes by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        }

        private static string GenerateCode(int size = 5)
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string code = "";
            for (int i = 1; i <= size; i++)
            {
                Random rand = new();
                int index = rand.Next(0, chars.Length);
                code += chars[index];
            }
            return code;
        }

        public bool IsEmailConfirm(string key, string code)
        {
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            _cache.TryGetValue(key, out string? verifycode);
            _cache.Remove(key);
            return code == verifycode;
        }
    }
}
