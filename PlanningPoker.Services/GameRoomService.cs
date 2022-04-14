using PlanningPoker.Data;
using PlanningPoker.Models;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Services;

public class GameRoomService : IGameRoomService
{
    public void CreateGameRoom(string roomName, GameRoom room)
    {
        DataRepository.GameRooms.Add(roomName, room);
    }

    public ICollection<GameRoom> ListGameRooms()
    {
        return DataRepository.GameRooms.Values;
    }

    public void AddPlayer(Player name)
    {
        throw new NotImplementedException();
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
}