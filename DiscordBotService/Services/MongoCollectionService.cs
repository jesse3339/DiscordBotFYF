using DiscordBotService.Enums;
using DiscordBotService.Models;
using DiscordBotService.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DiscordBotService.Services;

public class MongoCollectionService<T> where T : MongoModel
{
    private IMongoCollection<T> _modelCollection;
    
    public MongoCollectionService(MongoCollection collection)
    {
        var mongoDb = new MongoConnection().GetDatabase();
        var collectionName = string.Empty;
        switch (collection)
        {
            case MongoCollection.FightResults:
                collectionName = "fight_results";
                break;
            case MongoCollection.Clients:
                collectionName = "clients";
                break;
            case MongoCollection.Items:
                collectionName = "items";
                break;
            case MongoCollection.Stats:
                collectionName = "stats";
                break;
            default:
                //should throw exception
                break;
        }
        _modelCollection = mongoDb.GetCollection<T>(collectionName);
    }

    public ref IMongoCollection<T> GetCollection()
    {
        return ref _modelCollection;
    }
}