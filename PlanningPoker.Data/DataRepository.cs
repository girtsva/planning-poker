using PlanningPoker.Data.Interfaces;
using PlanningPoker.Models;

namespace PlanningPoker.Data;

//[UsedImplicitly]
public class DataRepository : IDataRepository
{
    //public static ICollection<Player> Players = new List<Player>();
    private static readonly IDictionary<string, GameRoom> GameRooms = new Dictionary<string, GameRoom>();

    public GameRoom CreateGameRoom(string roomName)
    {
        var gameRoom = new GameRoom(roomName);
        GameRooms.Add(roomName, gameRoom);
        return gameRoom;
    }

    public ICollection<GameRoom> ListGameRooms()
    {
        return GameRooms.Values;
    }

    public GameRoom? GetGameRoomByName(string roomName)
    {
        return GameRooms.ContainsKey(roomName) ? GameRooms[roomName] : null;
    }

    // public void AddPlayer(Player name)
    // {
    //     _gameRoom.Players.Add(name);
    // }

    public ICollection<Player> ListUsers()
    {
        throw new NotImplementedException();
    }

    public bool RoomNameExists(string roomName)
    {
        return GameRooms.ContainsKey(roomName);
    }

    public void DeleteAllRooms()
    {
        GameRooms.Clear();
    }

    public void DeleteRoom(string roomName)
    {
        GameRooms.Remove(roomName);
    }
}