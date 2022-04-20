using PlanningPoker.Models;

namespace PlanningPoker.Services.Interfaces;

public interface IGameRoomService
{
    public void CreateGameRoom(string roomName, GameRoom room);
    public ICollection<GameRoom> ListGameRooms();
    public GameRoom? GetGameRoomByName(string roomName);
    // public void AddPlayer(Player name);
    public ICollection<Player> ListUsers();
    public bool RoomNameExists(string roomName);
    public void ClearAllRooms();
    public void DeleteRoom(string roomName);
}