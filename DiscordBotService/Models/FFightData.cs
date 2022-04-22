namespace DiscordBotService.Models;

public class FFightData : MongoModel
{
    public List<string> AttackLog;
    public string WinnerObjRef;
    public string LoserObjRef;
    public DateTime Time;
}