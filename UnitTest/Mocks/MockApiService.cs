using RedditLogger.Interfaces;
using RedditLogger.Models;

namespace UnitTest.Mocks
{
    internal class MockApiService : MockBaseHttpService
    {
        public IRateLimit RateLimit { get; set; } = new RateLimit();

        public MockApiService() { }

        public Task<List<Post>> GetMostUpvotedPosts(string subreddit, CancellationToken stoppingToken)
        {
            List<Post> upvotes = new();

            if (subreddit.Equals("lifeprotips", StringComparison.OrdinalIgnoreCase))
            {
                upvotes.Add(new Post { Id = "abc", Url = "https://www.reddit.com/r/lifeprotips/abc", Author = "presentdifference100", Subreddit = "LifeProTips", Title = "LPT: keep your mouth shut, and don't volunteer information", Text = "I had a phone interview scheduled this morning, but accidentally slept through it." });
                upvotes.Add(new Post { Id = "def", Url = "https://www.reddit.com/r/lifeprotips/def", Author = "icarus9ll", Subreddit = "LifeProTips", Title = "LPT: Always tell a child who is wearing a helmet how cool you think their helmet is.It will encourage them to always wear it in the future." });
                upvotes.Add(new Post { Id = "xyz", Url = "https://www.reddit.com/r/lifeprotips/xyz", Author = "LampsLookingatyouTOO", Subreddit = "LifeProTips", Title = "LPT: Do not try to be the man your father would want you to be. Be the man you would like your son to be be. It more clearly defines your own convictions, desires, goals, and motivates you to be your best.", Text = "" });
            }
            return Task.FromResult(upvotes);
        }

        public Task<List<AuthorPostCount>> GetAuthorsWithMostPosts(string subreddit, CancellationToken stoppingToken)
        {
            List<AuthorPostCount> counts = new();

            if (subreddit.Equals("lifeprotips", StringComparison.OrdinalIgnoreCase))
            {
                counts.Add(new AuthorPostCount { Name = "LampsLookingatyouTOO", Subreddit = subreddit, Count = 3 });
                counts.Add(new AuthorPostCount { Name = "icarus9ll", Subreddit = subreddit, Count = 2 });
                counts.Add(new AuthorPostCount { Name = "presentdifference100", Subreddit = subreddit, Count = 1 });
            }

            return Task.FromResult(counts);
        }
    }
}
