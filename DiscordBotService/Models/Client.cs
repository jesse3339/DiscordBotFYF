using System.Collections;
using DiscordBotService.Utils;
using DSharpPlus.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DiscordBotService.Models;

public class Client : MongoModel
{
    public ulong DiscordId { get; set; }
    public string? Name { get; set; }
    public ArrayList? ItemRefs { get; set; } //object references to items collections
    public string? StatComponentRef { get; set; }


    public static async void Pit(DiscordUser attacker, DiscordUser target)
    {
        try
        {
            var db = new MongoConnection()._mongoConnection.GetDatabase("fight_your_friends");
            var collection = db.GetCollection<Client>("clients");

            var attackingClient = await collection.Find(model => model.DiscordId == attacker.Id).SingleAsync();
            var targetClient = await collection.Find(model => model.DiscordId == target.Id).SingleAsync();

            //attackingClient.Result
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}