namespace RedditLogger.Interfaces
{
    public interface IConsole
    {
        void Write(string? message = null);
        void WriteLine(string? message = null);
        void WriteLine(string format, params string[] messages);
        string? ReadLine();
        ConsoleKeyInfo ReadKey(bool intercept);
    }
}
