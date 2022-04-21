using DiscordBotService;
using DiscordBotService.Utils;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddHostedService<BotWorker>(); })
    .Build();

await host.RunAsync();