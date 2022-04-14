using PlanningPoker.Data;
using PlanningPoker.Models;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Services;

public class GameRoomService : IGameRoomService
{
    public void CreateGameRoom(GameRoom room)
    {
        DataStorage.GameRooms.Add(room);
    }

    public ICollection<GameRoom> ListGameRooms()
    {
        return DataStorage.GameRooms;
    }

    public void AddPlayer(Player name)
    {
        throw new NotImplementedException();
    }

    public ICollection<Player> ListUsers()
    {
        throw new NotImplementedException();
    }
}