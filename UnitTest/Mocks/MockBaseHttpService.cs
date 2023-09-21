using System.Net;

namespace UnitTest.Mocks
{
    internal abstract class MockBaseHttpService
    {
        public Task<HttpResponseMessage> Get(string url, CancellationToken stoppingToken)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }

        public Task<HttpResponseMessage> Post(string url, FormUrlEncodedContent content)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}
