using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.IO;
using Serilog;


namespace DiscordBotService.Utils;

public class MongoConnection
{
    public MongoClient _mongoConnection;
    private string DatabaseName { get; }

    
    public MongoConnection()
    {
        _mongoConnection = new MongoClient(Config.Configuration.GetSection("MongoDbSettings").GetValue<string>("ConnectionString"));
        DatabaseName = Config.Configuration.GetSection("MongoDbSettings").GetValue<string>("DatabaseName");
    }

    
    public IMongoDatabase GetDatabase( /*we could specify which database, but only one for now*/)
    {
        return _mongoConnection.GetDatabase(DatabaseName);
    }
}