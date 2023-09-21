using RedditLogger.Interfaces;

namespace RedditLogger.Shared
{
    public class RealConsole : IConsole
    {
        public void Write(string? message = null)
        {
            Console.Write(message);
        }

        public void WriteLine(string format, params string[] messages)
        {
            Console.WriteLine(format, messages);
        }

        public void WriteLine(string? message = null)
        {
            Console.WriteLine(message);
        }

        public string? ReadLine()
        {
            return Console.ReadLine();
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }
    }
}
