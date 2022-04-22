using System.Collections;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class StatComponent
{
    public string? OwnerRef;
    public int FightsWon;
    public int FightsLost;
    public int Level;
    public float Intelligence, Strength, Dexterity; //stats
    public float CurrentHealth;
    public float MaxHealth;
    public float Armor;
    public float MagicResist;

    public StatComponent()
    {
        Level = 1;
        CurrentHealth = 150.0f;
        MaxHealth = 150.0f;
        Armor = 25.0f;
        MagicResist = 25.0f;
        Dexterity = 10;
        Strength = 10;
        Intelligence = 10;
        FightsLost = 0;
        FightsWon = 0;
    }
}