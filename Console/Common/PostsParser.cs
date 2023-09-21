using RedditLogger.Interfaces;
using RedditLogger.Models;
using System.Text.Json;

namespace RedditLogger.Common
{
    public class PostsParser
    {
        private readonly string _subreddit;
        private readonly List<JsonElement> _children;

        public PostsParser(string subreddit, string getResult, IJsonParser parser)
        {
            _subreddit = subreddit;
            _children = parser.Parse(getResult);
        }

        public List<Post> Get()
        {
            List<Post> posts = new();

            foreach (var child in _children)
            {
                JsonElement data = child.GetProperty("data");

                Post post = new()
                {
                    Subreddit = _subreddit,
                    Id = data.GetProperty("id").GetString() ?? "",
                    Title = data.GetProperty("title").GetString() ?? "",
                    Text = data.GetProperty("selftext").GetString() ?? "",
                    Author = data.GetProperty("author").GetString() ?? "",
                    Score = data.GetProperty("score").GetInt32(),
                    Ups = data.GetProperty("ups").GetInt32(),
                    Downs = data.GetProperty("downs").GetInt32(),
                    Url = data.GetProperty("url").GetString() ?? ""
                };

                posts.Add(post);
            }

            posts.Sort((a, b) => b.Score.CompareTo(a.Score));

            return posts;
        }
    }
}
