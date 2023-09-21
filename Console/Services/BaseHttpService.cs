using System.Net.Http.Headers;

namespace RedditLogger.Services
{
    public abstract class BaseHttpService
    {
        private readonly IHttpClientFactory _clientFactory;
        private AuthenticationHeaderValue? _header;
        private readonly string _serviceName;

        public void SetAuthenticationHeader(string scheme, string token)
        {
            _header = new AuthenticationHeaderValue(scheme, token);
        }

        public BaseHttpService(IHttpClientFactory clientFactory, string serviceName)
        {
            _clientFactory = clientFactory;
            _serviceName = serviceName;
        }

        public async Task<HttpResponseMessage> Post(string url, FormUrlEncodedContent content)
        {
            HttpClient httpClient = _clientFactory.CreateClient(_serviceName);

            if (_header != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = _header;
            }

            HttpRequestMessage request = new(HttpMethod.Post, url)
            {
                Content = content
            };

            return await httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> Get(string url, CancellationToken stoppingToken)
        {
            HttpClient httpClient = _clientFactory.CreateClient(_serviceName);

            if (_header != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = _header;
            }

            HttpRequestMessage request = new(HttpMethod.Get, url);

            return await httpClient.SendAsync(request, stoppingToken);
        }
    }
}
