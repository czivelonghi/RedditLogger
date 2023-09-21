using System.Net.Http.Headers;

namespace RedditLogger.Interfaces
{
    public interface IRateLimit
    {
        double Remaining { get; set; }
        double Reset { get; set; }
        double Used { get; set; }

        void Set(HttpResponseHeaders headers);
    }
}