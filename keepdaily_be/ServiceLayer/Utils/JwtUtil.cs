using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace ServiceLayer.Utils
{
    public interface IJwtUtil
    {
        public string GenerateJwtToken(User user);
        public int? ValidateJwtToken(string? token);
        public string GenerateRefreshToken(User user);
    }
    public class JwtUtil : IJwtUtil
    {
        private readonly string _secret;
        private static readonly IDictionary<int, string> _refreshTokens = new Dictionary<int, string>();

        public JwtUtil(IConfiguration config)
        {
            _secret = config["JWTSecret"] ?? throw new NullReferenceException("Null JWTSecret");
        }
            
        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddSeconds(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int? ValidateJwtToken(string? token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        public string GenerateRefreshToken(User user)
        {
            if (_refreshTokens.ContainsKey(user.Id))
            {
                lock (_refreshTokens)
                {
                    _refreshTokens.Remove(user.Id);
                }
            }

            var refreshToken = Guid.NewGuid().ToString();
            lock (_refreshTokens)
            {
                _refreshTokens.Add(user.Id, refreshToken);
            }

            return refreshToken;
        }
    }
}
