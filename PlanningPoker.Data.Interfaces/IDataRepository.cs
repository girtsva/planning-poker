using PlanningPoker.Models;

namespace PlanningPoker.Data.Interfaces;

public interface IDataRepository
{
    GameRoom CreateGameRoom(string roomName);
    ICollection<GameRoom> ListGameRooms();
    GameRoom? GetGameRoomByName(string roomName);
    ICollection<Player> ListUsers();
    bool RoomNameExists(string roomName);
    void DeleteAllRooms();
    void DeleteRoom(string roomName);
}