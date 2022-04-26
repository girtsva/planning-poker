using PlanningPoker.Models;

namespace PlanningPoker.Data.Interfaces;

public interface IDataRepository
{
    /// <inheritdoc cref="M:PlanningPoker.Services.Interfaces.IGameRoomService.CreateGameRoom(string)" />
    GameRoom CreateGameRoom(string roomName);

    /// <inheritdoc cref="PlanningPoker.Services.Interfaces.IGameRoomService.ListGameRooms()" />
    ICollection<GameRoom> ListGameRooms();

    GameRoom? GetGameRoomByName(string roomName);
    GameRoom? AddPlayer(string roomId, Player name);
    ICollection<Player> ListUsers();
    bool RoomNameExists(string roomName);
    bool RoomIdExists(string roomId);
    void DeleteAllRooms();
    void DeleteRoom(string roomName);
}