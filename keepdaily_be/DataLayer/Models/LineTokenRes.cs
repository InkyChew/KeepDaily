using System.Text.Json.Serialization;

namespace DomainLayer.Models
{
    public class LineTokenRes
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
    }
}
