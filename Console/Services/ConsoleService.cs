using RedditLogger.Models;
using RedditLogger.Shared;
using Microsoft.Extensions.Options;
using RedditLogger.Common;

namespace RedditLogger.Services;

public class ConsoleService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly RealConsole _console = new();
    private readonly List<string> _subreddits;
    private AccessToken? _accessToken;
    private (string, string) _credentials;

    private readonly Action<List<Post>, RealConsole> writeMostUpvotes = (posts, console) => 
    {
        foreach (Post post in posts)
        {
            console.WriteLine("Most upvotes: {0,-20} {1,-150}", post.Subreddit, post.Title);
        }
    };

    private readonly Action<List<AuthorPostCount>, RealConsole> writeAuthorsWithMostPosts = (authors, console) =>
    {
        foreach (AuthorPostCount author in authors)
        {
            console.WriteLine("Most Posts: {0,-20} {1,-150}", author.Subreddit, author.Name + $"({author.Count})");
        }
    };

    public ConsoleService(IOptions<Application> options, IServiceProvider services)
    {
        _subreddits = options.Value.Subreddits.ToList();
        _services = services;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        return base.StartAsync(cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        return base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _services.CreateScope();
        LoginService? loginService = scope.ServiceProvider.GetRequiredService<LoginService>();
        
        do
        {
            CredentialsConsole prompt = new(_console);

            _credentials = prompt.GetLogin();

            _accessToken = await loginService.AuthenticateAsync(_credentials.Item1, _credentials.Item2);

            if (_accessToken != null) break;

            if (prompt.LoginAgain() == false)
                Environment.Exit(0);

        } while (true);

        ApiService apiService = scope.ServiceProvider.GetRequiredService<ApiService>();

        apiService.SetAuthenticationHeader("Bearer", _accessToken.Token);

        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (string subreddit in _subreddits)
            {
                try
                {
                    List<Post> topPosts = await apiService.GetTopPostsAsync(subreddit, stoppingToken, TimePeriod.Day, 10);

                    writeMostUpvotes(topPosts, _console);

                    List<AuthorPostCount> topAuthors = await apiService.GetAuthorsWithMostPostsAsync(subreddit, stoppingToken, TimePeriod.Day, 10);

                    writeAuthorsWithMostPosts(topAuthors, _console);

                }
                catch (Exception ex) {
                    _console.WriteLine(ex.Message);
                }
            }
        }
    }
}
