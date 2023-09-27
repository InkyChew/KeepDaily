using System.Text.Json.Serialization;

namespace DomainLayer.Models
{
    public class AuthenticateUser
    {
        public int Id { get; set; }
        public UserLevel UserLevel { get; set; }
        public string AccessToken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public AuthenticateUser(User user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            UserLevel = user.UserLevel;
            AccessToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
