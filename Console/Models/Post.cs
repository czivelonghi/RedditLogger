namespace RedditLogger.Models
{
    public class Post
    {
        public required string Id { get; set; }
        public required string Subreddit { get; set; }
        public required string Title { get; set; }
        public string? Text { get; set; }
        public required string Author { get; set; }
        public int Score { get; set; }
        public int Ups { get; set; }
        public int Downs { get; set; }
        public required string Url { get; set; }
        public Post() { }
    }
}
