using RedditLogger.Models;
using System.Globalization;
using System.Net;
using System.Threading.RateLimiting;

namespace RedditLogger.Handlers
{
    internal class RateLimitHandler : DelegatingHandler
    {
        private readonly RateLimiter _rateLimiter;
        private readonly RateLimit _redditLimit = new();

        public RateLimitHandler(RateLimiter limiter)
        {
            _rateLimiter = limiter;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;

            using RateLimitLease lease = await _rateLimiter.AcquireAsync(1, cancellationToken);
            if (lease.IsAcquired)
            {
                response = await base.SendAsync(request, cancellationToken);

                _redditLimit.Set(response.Headers);

                return response;
            }

            response = new (HttpStatusCode.TooManyRequests);
            if (lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
            {
                string waitSeconds = ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);

                if (_redditLimit.Remaining == 0)
                {
                    waitSeconds = _redditLimit.Reset.ToString();
                }
                response.Headers.Add("Retry-After", waitSeconds);
            }
            return response;
        }
    }

}
