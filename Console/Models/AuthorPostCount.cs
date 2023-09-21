namespace RedditLogger.Models
{
    public class AuthorPostCount
    {
        public required string Subreddit { get; set; }
        public required string Name { get; set; }
        public int Count { get; set; }
        public AuthorPostCount() { }
    }
}
