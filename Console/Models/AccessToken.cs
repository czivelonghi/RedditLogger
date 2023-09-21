using System.Text.Json.Serialization;

namespace RedditLogger.Models
{
    public class AccessToken
    {
        [JsonPropertyName("access_token")]
        public required string Token { get; set; }
        [JsonPropertyName("token_type")]
        public required string TokenType { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonPropertyName("scope")]
        public required string Scope { get; set; }

        public AccessToken() { }
    }
}
