using Database;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DC_Bot;

// ReSharper disable once ClassNeverInstantiated.Global
internal class Program
{
    private static IConfiguration? _configuration;
    private static IServiceProvider? _services;

    private static readonly DiscordSocketConfig SocketConfig = new()
    {
        GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers,
        AlwaysDownloadUsers = true,
    };

    private static readonly InteractionServiceConfig InteractionServiceConfig = new()
    {
        DefaultRunMode = RunMode.Async,
        // LocalizationManager = new ResxLocalizationManager("InteractionFramework.Resources.CommandLocales", Assembly.GetEntryAssembly(), new CultureInfo("en-US"))
    };

    public static async Task Main(string[] args)
    {
        _configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables(prefix: "DC_")
            // ReSharper disable once StringLiteralTypo
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        if (string.IsNullOrWhiteSpace(_configuration["token"]))
        {
            throw new NullReferenceException("Token is missing");
        }

        _services = new ServiceCollection()
            .AddSingleton(_configuration)
            .AddSingleton(SocketConfig)
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton(x =>
                new InteractionService(x.GetRequiredService<DiscordSocketClient>(), InteractionServiceConfig))
            .AddSingleton<InteractionHandler>()
            .AddDbContext<DatabaseContext>()
            .BuildServiceProvider();

        var client = _services.GetRequiredService<DiscordSocketClient>();

        client.Log += LogAsync;

        // Here we can initialize the service that will register and execute our commands
        await _services.GetRequiredService<InteractionHandler>()
            .InitializeAsync();

        // Bot token can be provided from the Configuration object we set up earlier
        await client.LoginAsync(TokenType.Bot, _configuration["token"]);
        await client.StartAsync();

        client.Ready += () =>
        {
            Console.WriteLine($"Bot is connected as: {client.CurrentUser.Username} | {client.CurrentUser.Id}");
            return Task.CompletedTask;
        };

        // Never quit the program until manually forced to.
        await Task.Delay(Timeout.Infinite);
    }

    private static Task LogAsync(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        return Task.CompletedTask;
    }
}