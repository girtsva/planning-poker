using PlanningPoker.Models;

namespace PlanningPoker.Data.Interfaces;

public interface IDataRepository
{
    /// <inheritdoc cref="M:PlanningPoker.Services.Interfaces.IGameRoomService.CreateGameRoom(string)" />
    GameRoom CreateGameRoom(string roomName);

    /// <inheritdoc cref="PlanningPoker.Services.Interfaces.IGameRoomService.ListGameRooms()" />
    ICollection<GameRoom> ListGameRooms();

    GameRoom? GetGameRoomById(string roomId);
    GameRoom? AddPlayer(string roomId, Player player);
    ICollection<Player> ListUsers();
    bool RoomNameExists(string roomName);
    bool RoomIdExists(string roomId);
    void DeleteAllRooms();
    void DeleteRoom(string roomId);
}