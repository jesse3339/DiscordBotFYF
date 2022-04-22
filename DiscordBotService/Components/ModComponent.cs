using System.Text;
using System.Text.RegularExpressions;

namespace DiscordBotService.Components;

public class ModComponent
{
    public string? ModString;
    public int Tier;
    public float ValueOne;
    public float? ValueTwo;
    public string Type;

    public ModComponent(string type, int tier, float val1, float? val2)
    {
        Type = type;
        Tier = tier;
        ValueOne = val1;
        ValueTwo = val2;
        ModString = ToString();
    }

    public sealed override string ToString()
    {
        var splitStr = Regex.Split(Type, @"(?<=[A-Za-z])(?=[A-Z][a-z])|(?<=[a-z0-9])(?=[0-9]?[A-Z])").ToList();
        
        var sb = new StringBuilder();
        if (splitStr[0] == "Added")
        {
            sb.Append($"Adds {ValueOne} to ");
            if (ValueTwo is not null)
            {
                sb.Append($"{ValueTwo} to");
            }
        } else if (splitStr[0] == "Increased")
        {
            sb.Append($"{ValueOne}% increased ");
        }

        if (splitStr[2] == "Damage")
        {
            sb.Append(splitStr[1]);
            sb.Append(" damage");
        }
        else
        {
            sb.Append(splitStr[1]);
        }
        
        return sb.ToString();
    }
}