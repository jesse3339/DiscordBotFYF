using System.Collections;
using DiscordBotService.Utils;
using DSharpPlus.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DiscordBotService.Models;

public class FClient : MongoModel
{
    public FClient(ulong discordId, string? name)
    {
        DiscordId = discordId;
        Name = name;
        Stats = new StatComponent();
        ItemRefs = new List<string>();
    }

    public ulong DiscordId { get; set; }
    public string? Name { get; set; }
    public List<string> ItemRefs { get; set; } //object references to items collections
    public StatComponent? Stats { get; set; }
    
    
    
}