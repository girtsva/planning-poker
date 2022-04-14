using PlanningPoker.Models;

namespace PlanningPoker.Services.Interfaces;

public interface IGameRoomService
{
    public void CreateGameRoom(GameRoom room);
    public ICollection<GameRoom> ListGameRooms();
    public void AddPlayer(Player name);
    public ICollection<Player> ListUsers();
}