using RedditLogger.Shared;
using UnitTest.Common;
using UnitTest.Mocks;

namespace UnitTest
{
    [TestCaseOrderer("UnitTest.Common.PriorityOrderer", "UnitTest")]
    public class PromptConsoleTests
    {
        private readonly MockConsole _console;

        public PromptConsoleTests()
        {
            _console = new();
        }

        [Fact, TestPriority(1)]
        public void KeyedUsernameMatchesConsole()
        {
            string username = "LampsLookingatyouTOO";

            _console.SetInput(username);

            CredentialsConsole prompt = new(_console);

            string output = prompt.GetUsername();

            Assert.Equal(username, output);
        }

        [Fact, TestPriority(2)]
        public void YesKeyLoginAgainIsTrue()
        {
            string input = "Y";

            _console.SetInput(input);

            CredentialsConsole prompt = new(_console);

            bool again = prompt.LoginAgain();

            Assert.True(again);
        }

        [Fact, TestPriority(3)]
        public void NoKeyLoginAgainIsFalse()
        {
            string input = "N";

            _console.SetInput(input);

            CredentialsConsole prompt = new(_console);

            bool again = prompt.LoginAgain();

            Assert.False(again);
        }

    }

}