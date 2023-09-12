using Microsoft.AspNetCore.Mvc;

namespace DomainLayer.Models
{
    public class AuthGoogle
    {
        [ModelBinder(Name = "credential")]
        public string Credential { get; set; } = null!;

        [ModelBinder(Name = "g_csrf_token")]
        public string Token { get; set; } = null!;        
    }
}
