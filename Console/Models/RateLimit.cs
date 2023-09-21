using RedditLogger.Interfaces;
using System.Net.Http.Headers;

namespace RedditLogger.Models
{
    public class RateLimit : IRateLimit
    {
        public double Used { get; set; }
        public double Remaining { get; set; }
        public double Reset { get; set; }
        private readonly string[] keys = { "x-ratelimit-remaining", "x-ratelimit-used", "x-ratelimit-reset" };

        public void Set(HttpResponseHeaders headers)
        {
            Dictionary<string, double> limits = headers
                .Where(h => keys.Contains(h.Key, StringComparer.OrdinalIgnoreCase))
                .ToDictionary(h => h.Key, h => {
                    return double.TryParse(h.Value.First(), out double result) ? result : 0;
                });

            Used        = limits["x-ratelimit-used"];
            Remaining   = limits["x-ratelimit-remaining"];
            Reset       = limits["x-ratelimit-reset"];
        }
    }
}
