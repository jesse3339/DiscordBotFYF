using System.Text;
using System.Text.RegularExpressions;
using DiscordBotService.Components;

namespace DiscordBotService.Libs;

public static class ModLib
{
    private static readonly List<string> AugTypes;
    private static readonly List<string> ModTypes;
    private static readonly Dictionary<int, int> TierWeights;
    private static readonly int[,] AddedTierValueBase;
    private static readonly int[,] IncreasedTierValueBase;
    private const float DamageValueMulti = 0.75f;
    private const float StatValueMulti = 0.25f;
    private const float DefenseValueMulti = 0.5f;
    
    static ModLib()
    {
        AugTypes = new List<string>()
        {
            @"\bincr",      //% increased (1 val)
            @"\badd",       //flat added  (2 vals)
        };

        ModTypes = new List<string>()
        {
            @"\bphys",          //Physical
            @"\bm",             //Magic
            @"\barmor",         //Armor
            @"\bmag(?=.*\bres)"
        };
        


        TierWeights = new Dictionary<int, int>()
        {
            {0, 1024},
            {1, 512},
            {2, 256},
            {3, 128},
            {4, 64},
            {5, 32},
            {6, 16}
        };
    }


    public static ModComponent GetValidMod(List<string> currentModifiers)
    {
        var temp = AugTypes;
        foreach (var mod in currentModifiers)
        {
            
        }
    }

    public static KeyValuePair<string, float[]> ModParse(string modifier)
    {
        var temp = new KeyValuePair<string, float[]>();
        var keyStr = string.Empty;
        var fVals = new float[1];

        var sb = new StringBuilder();
        var vals = Regex.Matches(modifier, @"\b\d+"); //this grabs all numbers in string
        var rawAugType = Regex.Match(modifier, @"incr|\badd").Value;
        var rawType = 
        return temp;
    }

    
    /// <summary>
    /// Take a list of modifier strings, parse and compile a dictionary with the total for each modifier
    /// </summary>
    /// <param name="modifiers">Aggregation of all modifiers</param>
    /// <returns>dictionary of totals for each mod from all modifiers on all items</returns>
    public static Dictionary<string, float[]> CompileFlags(List<string> modifiers)
    {
        var temp = new Dictionary<string, float[]>();

        foreach (var modStr in modifiers)
        {
            var f = ModParse(modStr);
            if (temp.ContainsKey(f.Key))
            {
                for (var i = 0; i < temp[f.Key].Length; i++)
                {
                    temp[f.Key][i] += f.Value[i];
                }
            }
            else
            {
                temp.Add(f.Key, f.Value);
            }
        }

        return temp;
    }

    
    /// <summary>
    /// Match a single string against the specified static regex checklist
    /// </summary>
    /// <param name="input">Input string</param>
    /// <param name="list">List to check against</param>
    /// <returns>the matched value, NULL if none found</returns>
    public static string RegexMatchAgainst(string input, List<string> list)
    {
        foreach (var expr in list)
        {
            string temp;
            if ((temp = Regex.Match(input, expr).Value) != string.Empty)
            {
                return temp;
            }
        }

        return "NULL";
    }
}