using RedditLogger.Handlers;
using RedditLogger.Models;
using RedditLogger.Services;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.RateLimiting;

AppSettings? settings = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build()
    .Get<AppSettings>();

RateLimiter rateLimiter = new TokenBucketRateLimiter(new()
{
    TokenLimit = 2,
    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
    QueueLimit = 2,
    ReplenishmentPeriod = TimeSpan.FromSeconds(2),
    TokensPerPeriod = 2,
    AutoReplenishment = true
});

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, config) => config.AddJsonFile("appsettings.json", optional: false))
    .ConfigureServices((hostContext, services) =>
    {
        services.AddOptions();
        services.Configure<Application>(options => hostContext.Configuration.GetSection("Application").Bind(options));
        services.AddHostedService<ConsoleService>();
        services.AddTransient<LoginService>();
        services.AddTransient<ApiService>();
        services.AddHttpClient(nameof(LoginService), c =>
        {
            c.BaseAddress = new Uri(settings.Application.LoginUri);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            c.DefaultRequestHeaders.Add("User-Agent", settings.Application.UserAgent);
            byte[]? basicAuthorization = Encoding.UTF8.GetBytes($"{settings.Application.Id}:{settings.Application.Secret}");
            c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(basicAuthorization));
        });
        services.AddHttpClient(nameof(ApiService), c =>
        {
            c.BaseAddress = new Uri(settings.Application.ApiUri);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            c.DefaultRequestHeaders.Add("User-Agent", settings.Application.UserAgent);
        })
        .AddHttpMessageHandler(() => new RateLimitHandler(rateLimiter));
    })
    .Build()
    .Run();
