using RedditLogger.Interfaces;

namespace UnitTest.Mocks
{
    public class MockConsole : IConsole
    {
        private readonly Queue<string> _stringQueue = new();
        private char _key;
        public string Output = "";

        public void SetInput(string input)
        {
            _stringQueue.Enqueue(input);
        }

        public void SetInput(char input)
        {
            _key = input;
        }

        public void Write(string? message = null)
        {
            Output += message;
        }

        public void WriteLine(string? message)
        {
            Output += message + "\n";
        }

        public string? ReadLine()
        {
            return _stringQueue.Dequeue();
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return new(_key, ConsoleKey.Enter, false, false, false);
        }

        public void WriteLine(string format, params string[] messages)
        {
            Output += "";
        }
    }
}
