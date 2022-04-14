using PlanningPoker.Models;

namespace PlanningPoker.Data;

public static class DataStorage
{
    public static ICollection<Player> Players = new List<Player>();
    public static ICollection<GameRoom> GameRooms = new List<GameRoom>();

    public static bool Exists(string name)
    {
        return GameRooms.Any(room => room.Name.ToLower().Trim() == name.ToLower().Trim());
    }
}