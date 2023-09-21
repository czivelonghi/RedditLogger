namespace RedditLogger.Models
{
    public class Application
    {
        public required string LoginUri { get; set; }
        public required string ApiUri { get; set; }
        public required string UserAgent { get; set; }
        public required string Id { get; set; }
        public required string Secret { get; set; }
        public required IEnumerable<string> Subreddits { get; set;}
    }

    public class AppSettings
    {
        public required Application Application { get; set; }
        public required IEnumerable<string> Subreddits { get; set; }
    }
}
