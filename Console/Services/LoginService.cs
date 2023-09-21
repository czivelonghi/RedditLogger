using RedditLogger.Models;
using System.Text.Json;

namespace RedditLogger.Services
{
    internal class LoginService : BaseHttpService
    {
        private readonly ILogger<LoginService> _logger;

        public LoginService(IHttpClientFactory clientFactory, ILogger<LoginService> logger) : base(clientFactory, nameof(LoginService))
        {
            _logger = logger;
        }

        public async Task<AccessToken?> AuthenticateAsync(string username, string password)
        {
            AccessToken? token = null;

            try
            {
                FormUrlEncodedContent content = new(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });

                HttpResponseMessage response = await Post("access_token", content);
                response.EnsureSuccessStatusCode();

                string responseString = await response.Content.ReadAsStringAsync();

                if (responseString.Contains("invalid_grant", StringComparison.OrdinalIgnoreCase) == false)
                {
                    token = JsonSerializer.Deserialize<AccessToken>(responseString);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return token;
        }
    }
}
