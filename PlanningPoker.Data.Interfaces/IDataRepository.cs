using PlanningPoker.Models;

namespace PlanningPoker.Data.Interfaces;

public interface IDataRepository
{
    /// <inheritdoc cref="M:PlanningPoker.Services.Interfaces.IGameRoomService.CreateGameRoom(string)" />
    GameRoom CreateGameRoom(string roomName);

    /// <inheritdoc cref="PlanningPoker.Services.Interfaces.IGameRoomService.ListGameRooms()" />
    ICollection<GameRoom> ListGameRooms();

    GameRoom? GetGameRoomByName(string roomName);
    ICollection<Player> ListUsers();
    bool RoomNameExists(string roomName);
    void DeleteAllRooms();
    void DeleteRoom(string roomName);
}