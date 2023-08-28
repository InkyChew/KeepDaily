using System.Text.Json.Serialization;

namespace DomainLayer.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public UserLevel Level { get; set; }
        public string AccessToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            Level = user.UserLevel;
            AccessToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
