using RedditLogger.Common;
using RedditLogger.Interfaces;
using RedditLogger.Models;

namespace RedditLogger.Services
{
    internal class ApiService : BaseHttpService
    {
        private readonly ILogger<ApiService> _logger;

        public ApiService(IHttpClientFactory clientFactory, ILogger<ApiService> logger) : base(clientFactory, nameof(ApiService))
        {
            _logger = logger;
        }

        private async Task<List<Post>> GetCategoryPosts(string subreddit, string category, CancellationToken stoppingToken, TimePeriod timePeriod, int? limit = null)
        {
            List<Post> posts = new();

            try
            {
                string time = "all";
                switch (timePeriod)
                {
                    case TimePeriod.Now: time = "hour"; break;
                    case TimePeriod.Day: time = "day"; break;
                    case TimePeriod.Week: time = "week"; break;
                    case TimePeriod.Month: time = "month"; break;
                    case TimePeriod.Year: time = "year"; break;
                    default: break;
                }

                string url = $"r/{subreddit}/{category}/.json?t={time}";
                if (limit is not null)
                {
                    url += $"&limit={limit}";
                }

                HttpResponseMessage response = await Get(url, stoppingToken);
                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync(stoppingToken);

                IJsonParser jsonParser = new JsonParser();

                PostsParser parser = new(subreddit, result, jsonParser);

                posts.AddRange(parser.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return posts;
        }

        public async Task<List<Post>> GetNewPostsAsync(string subreddit, CancellationToken stoppingToken, TimePeriod timePeriod, int? limit = null)
        {
            return await GetCategoryPosts(subreddit, "new", stoppingToken, timePeriod, limit);
        }

        public async Task<List<Post>> GetTopPostsAsync(string subreddit, CancellationToken stoppingToken, TimePeriod timePeriod, int? limit = null)
        {
            return await GetCategoryPosts(subreddit, "top", stoppingToken, timePeriod, limit);
        }

        public async Task<List<AuthorPostCount>> GetAuthorsWithMostPostsAsync(string subreddit, CancellationToken stoppingToken, TimePeriod timePeriod, int top = 10)
        {
            List<Post> posts = await GetCategoryPosts(subreddit, "new", stoppingToken, timePeriod);

            List<AuthorPostCount> counts = posts
                .GroupBy(p => p.Author)
                .Select(p => new AuthorPostCount
                {
                    Subreddit = subreddit,
                    Name = p.Key,
                    Count = p.Count()
                })
                .OrderByDescending(c => c.Count)
                .Take(top)
                .ToList();

            return counts;
        }

    }
}
