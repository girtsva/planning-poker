using PlanningPoker.Models;

namespace PlanningPoker.Data.Interfaces;

public interface IDataRepository
{
    /// <inheritdoc cref="M:PlanningPoker.Services.Interfaces.IGameRoomService.CreateGameRoom(string)" />
    GameRoom CreateGameRoom(string roomName);

    /// <inheritdoc cref="PlanningPoker.Services.Interfaces.IGameRoomService.ListGameRooms()" />
    ICollection<GameRoom> ListGameRooms();

    GameRoom? GetGameRoomById(string roomId);
    // GameRoom? AddPlayer(string roomId, Player player);
    GameRoom? AddPlayer(string roomId, string playerName);
    ICollection<Player> ListUsers();
    GameRoom? RemovePlayer(string roomId, string playerId);
    bool PlayerNameExists(string playerName);
    bool PlayerIdExists(string playerId);
    bool RoomNameExists(string roomName);
    bool RoomIdExists(string roomId);
    void DeleteAllRooms();
    void DeleteRoom(string roomId);
}