public class Program
{
    public static Task Main() => new Program().MainAsync();
    public async Task MainAsync()
    {


        using IHost host = Host.CreateDefaultBuilder()
        .ConfigureServices((_, services) =>
         services
         .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
         {
             GatewayIntents = GatewayIntents.AllUnprivileged,
             AlwaysDownloadUsers = true
         }
         ))
         .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
         .AddSingleton<MusicPlayer.InteractionHandler>()
         .AddSingleton(x => new CommandService())
         )
         .Build();
        await RunAsync(host);
    }
    public async Task RunAsync(IHost host)
    {
        using IServiceScope serviceScope = host.Services.CreateScope();
        IServiceProvider provider = serviceScope.ServiceProvider;

        var _client = provider.GetRequiredService<DiscordSocketClient>();
        var slashCommands = provider.GetRequiredService<InteractionService>();
        await provider.GetRequiredService<MusicPlayer.InteractionHandler>().InitializeAsync();

        _client.Log += async (LogMessage messages) => Console.WriteLine(messages.Message);
        slashCommands.Log += async (LogMessage messages) => Console.WriteLine(messages.Message);

        _client.Ready += async () =>
        {
            Console.WriteLine($"Login as{_client.CurrentUser.Username.ToString()}");
            await slashCommands.RegisterCommandsGloballyAsync(true);
        };
        var token = ConfigurationManager.AppSettings.Get("TOKEN");
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }
}
