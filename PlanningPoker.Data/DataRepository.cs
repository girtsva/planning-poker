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

    public GameRoom? AddPlayer(string roomId, Player name)
    {
        var gameRoom = GameRooms.Values.FirstOrDefault(gameRoom => gameRoom.Id == roomId);
        gameRoom?.Players.Add(name);
        return gameRoom;
    }

    public ICollection<Player> ListUsers()
    {
        throw new NotImplementedException();
    }

    public bool RoomNameExists(string roomName)
    {
        return GameRooms.ContainsKey(roomName);
    }
    
    public bool RoomIdExists(string roomId)
    {
        return GameRooms.Values.Any(gameRoom => gameRoom.Id == roomId);
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