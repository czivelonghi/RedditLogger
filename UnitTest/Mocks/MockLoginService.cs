using RedditLogger.Interfaces;
using RedditLogger.Models;

namespace UnitTest.Mocks
{
    internal class MockLoginService : MockBaseHttpService
    {
        public IRateLimit RateLimit { get; set; } = new RateLimit();

        public MockLoginService() { }

        public AccessToken? Authenticate(string username, string password)
        {
            if (username == null || password == null)
                return null;

            return new AccessToken
            {
                TokenType = "bearer",
                Token = "xyz123",
                ExpiresIn = 600,
                Scope = ""
            };
        }
    }
}
