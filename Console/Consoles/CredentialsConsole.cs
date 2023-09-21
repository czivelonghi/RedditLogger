using RedditLogger.Interfaces;

namespace RedditLogger.Shared
{
    public class CredentialsConsole
    {
        private readonly IConsole _console;

        public CredentialsConsole(IConsole console)
        {
            _console = console;
        }

        public string GetUsername()
        {
            string username;

            do
            {
                _console.Write("Username: ");
                username = _console.ReadLine() ?? "";
            } while (username.Length == 0);

            return username;
        }

        public string GetPassword()
        {
            string password;

            do
            {
                password = "";
                
                _console.Write("Password: ");

                while (true)
                {
                    ConsoleKeyInfo key = _console.ReadKey(true);
                    
                    if (key.Key == ConsoleKey.Enter)
                    {
                        _console.WriteLine();
                        break;
                    }
                    password += key.KeyChar;
                }
            } while (password.Length == 0);

            return password;
        }

        public (string Username, string Password) GetLogin()
        {
            return (GetUsername(), GetPassword());
        }

        public bool LoginAgain()
        {
            _console.Write($"Couldn't login... Try Again (Y/N)?");
            string input = _console.ReadLine() ?? "";

            return input.Equals("y", StringComparison.OrdinalIgnoreCase);
        }
    }
}
