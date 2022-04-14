using PlanningPoker.Models;

namespace PlanningPoker.Data;
//[UsedImplicitly]
public class DataRepository
{
    //public static ICollection<Player> Players = new List<Player>();
    public static readonly ICollection<GameRoom> GameRooms = new List<GameRoom>();

    public bool Exists(string name)
    {
        return GameRooms.Any(room => room.Name.ToLower().Trim() == name.ToLower().Trim());
    }
}