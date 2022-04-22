using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DiscordBotService.Models;

public class FItem : MongoModel
{
    public float BaseDamage;
    public List<string> Modifiers;


    public static FItem CreateRandom()
    {
        var tempItem = new FItem()
        {
            BaseDamage = Random.Shared.Next(1,5)
        };
        
        
        return tempItem;
    }
}