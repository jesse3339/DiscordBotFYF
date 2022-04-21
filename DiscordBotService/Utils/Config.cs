namespace DiscordBotService.Utils;

public static class Config
{
    public static IConfiguration Configuration;

    
    static Config()
    {
        Initialize();
    }

    
    public static void Initialize()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        Configuration = builder.Build();
    }
}