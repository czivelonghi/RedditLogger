using RedditLogger.Models;
using UnitTest.Common;
using UnitTest.Mocks;

namespace UnitTest
{
    [TestCaseOrderer("UnitTest.Common.PriorityOrderer", "UnitTest")]
    public class ApiServiceTests
    {
        [Fact, TestPriority(1)]
        public void MostUpvotedPostsHasCounts()
        {
            MockApiService mock = new();

            List<Post> posts = mock.GetMostUpvotedPosts("lifeprotips", new CancellationToken()).Result.ToList();

            Assert.True(posts.Count > 0);
        }

        [Fact, TestPriority(2)]
        public void AuthorsWithMostPostsHasCounts()
        {
            MockApiService mock = new();

            List<AuthorPostCount> count = mock.GetAuthorsWithMostPosts("lifeprotips", new CancellationToken()).Result.ToList();

            Assert.True(count.Count > 0);
        }

        [Fact, TestPriority(3)]
        public void HasValidRateLimitValues()
        {
            MockApiService mock = new();
            HttpResponseMessage response = new();

            double remaining = 599, reset = 580, used = 1;

            response.Headers.Add("x-ratelimit-remaining", remaining.ToString());
            response.Headers.Add("x-ratelimit-reset", reset.ToString());
            response.Headers.Add("x-ratelimit-used", used.ToString());

            mock.RateLimit.Set(response.Headers);

            Assert.Equal(remaining, mock.RateLimit.Remaining);
            Assert.Equal(reset, mock.RateLimit.Reset);
            Assert.Equal(used, mock.RateLimit.Used);
        }

        [Fact, TestPriority(4)]
        public void MissingRateLimitsDefaultsToZero()
        {
            MockApiService mock = new();
            HttpResponseMessage response = new();

            response.Headers.Add("x-ratelimit-remaining", "");
            response.Headers.Add("x-ratelimit-reset", "");
            response.Headers.Add("x-ratelimit-used", "");

            mock.RateLimit.Set(response.Headers);

            Assert.Equal(0, mock.RateLimit.Remaining);
            Assert.Equal(0, mock.RateLimit.Reset);
            Assert.Equal(0, mock.RateLimit.Used);
        }
    }
}
