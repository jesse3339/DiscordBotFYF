using DiscordBotService.Commands;
using DiscordBotService.Utils;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using MongoDB.Driver;
using Serilog;
using Serilog.Core;


namespace DiscordBotService;

public class BotWorker : BackgroundService
{
    private ILoggerFactory _loggerFactory;
    private DiscordClient _discordClient;
    private CommandsNextExtension _commandsNextConfiguration;
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        Config.Initialize(); // always initialize first
        InitLogger();
        InitBot();
        InitCommands();

        await _discordClient.ConnectAsync();
    }

    private void InitLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
        _loggerFactory = new LoggerFactory().AddSerilog();
    }

    private void InitCommands()
    {
        //initialize commands
        _commandsNextConfiguration = _discordClient.UseCommandsNext(new CommandsNextConfiguration()
        {
            StringPrefixes = new[] {"$"}
        });

        _commandsNextConfiguration.RegisterCommands<BasicCommands>();
    }


    protected void InitBot()
    {
        var botToken = Config.Configuration.GetSection("DiscordSettings").GetValue<string>("BotToken");
        //initialize discord bot
        _discordClient = new DiscordClient(new DiscordConfiguration()
        {
            Token = botToken,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged,
            LoggerFactory = _loggerFactory
        });

        Log.Information($"Bot Online {DateTime.Now}");
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _discordClient.DisconnectAsync();
        _discordClient.Dispose();
        Log.CloseAndFlush();
    }
}