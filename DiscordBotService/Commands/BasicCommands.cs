using System.Drawing;
using DiscordBotService.Enums;
using DiscordBotService.Models;
using DiscordBotService.Services;
using DiscordBotService.Utils;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using Serilog;


namespace DiscordBotService.Commands;

public class BasicCommands : BaseCommandModule
{
    [Command("stats")]
    [Description("Retrieve stats of a user")]
    public async Task Stats(CommandContext ctx)
    {
        await ctx.TriggerTypingAsync();
        var sCollection = new MongoCollectionService<StatComponent>(MongoCollection.Stats).GetCollection();
        var cCollection = new MongoCollectionService<Client>(MongoCollection.Clients).GetCollection();
        var userObj = await cCollection.Find(u => u.DiscordId == ctx.Member.Id).SingleAsync();
        var statComponent = await sCollection.Find(m => m.Id == userObj.StatComponentRef).SingleAsync();
        
        var builder = new DiscordEmbedBuilder();
        builder.Color = new DSharpPlus.Entities.Optional<DiscordColor>(DiscordColor.Red);
        builder.Title = $"{ctx.User.Username}'s Stats";
        builder.AddField("Level:", $"{statComponent.Level}", true);
        builder.AddField("Fights Won:", $"{statComponent.FightsWon}/{statComponent.FightsLost + statComponent.FightsWon} || " +
                                        $"{Math.Round((float)statComponent.FightsWon/(statComponent.FightsLost + statComponent.FightsWon), 2)}%", true);
        builder.AddField("Health:", $"{statComponent.CurrentHealth}/{statComponent.MaxHealth}");
        
        await new DiscordMessageBuilder().WithContent($"Here are {ctx.User.Mention}'s stats!").WithEmbed(builder.Build()).WithAllowedMentions(new IMention[] {new UserMention(ctx.User)}).SendAsync(ctx.Channel);
    }

    
    [Command("register")]
    [Description("Register your character.")]
    public async Task RegisterCommand(CommandContext ctx)
    {
        try
        {
            var collection = new MongoCollectionService<Client>(MongoCollection.Clients).GetCollection();

            if (ctx.Member != null)
            {
                var model = new Client()
                {
                    DiscordId = ctx.Member.Id,
                    Name = ctx.Member.DisplayName
                };
                if (!await collection.Find(m => m.DiscordId == ctx.Member.Id).AnyAsync())
                {
                    await collection.InsertOneAsync(model); //insert user
                    
                    var statCollection = new MongoCollectionService<StatComponent>(MongoCollection.Stats).GetCollection();
                    var baseStatComponent = new StatComponent(ownerRef: model.Id);
                    await statCollection.InsertOneAsync(baseStatComponent); //insert user stats
                    
                    var filter = Builders<Client>.Filter.Eq("Id", model.Id);
                    var update = Builders<Client>.Update.Set("StatComponentRef", baseStatComponent.Id);
                    await collection.UpdateOneAsync(filter, update); //establish bi-directional reference
                    
                    await ctx.RespondAsync("User Created! Check out your base stats with the 'stats' command!");
                }
                else
                {
                    await ctx.RespondAsync("User already exists!");
                }
            }
        }
        catch (Exception e)
        {
            Log.Warning("Failed to add user and connect to database!");
            await ctx.RespondAsync(
                "Sorry, but I could not connect to the database to register you.  Please try again later");
        }
    }
}