using PlanningPoker.Data;
using PlanningPoker.Models;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Services;

public class GameRoomService : IGameRoomService
{
    private readonly GameRoom _gameRoom;

    public GameRoomService(GameRoom gameRoom)
    {
        _gameRoom = gameRoom;
    }

    public void CreateGameRoom(string roomName, GameRoom room)
    {
        DataRepository.GameRooms.Add(roomName, room);
    }

    public ICollection<GameRoom> ListGameRooms()
    {
        return DataRepository.GameRooms.Values;
    }
    
    public GameRoom GetGameRoomByName(string roomName)
    {
        return DataRepository.GameRooms.FirstOrDefault(kvp=> kvp.Key.Contains(roomName)).Value;
    }

    public void AddPlayer(Player name)
    {
        _gameRoom.Players.Add(name);
    }

    public ICollection<Player> ListUsers()
    {
        throw new NotImplementedException();
    }

    public bool RoomNameExists(string roomName)
    {
        return DataRepository.GameRooms.ContainsKey(roomName);
    }

    public void ClearAllRooms()
    {
        DataRepository.GameRooms.Clear();
    }

    public void DeleteRoom(string roomName)
    {
        DataRepository.GameRooms.Remove(roomName);
    }
}