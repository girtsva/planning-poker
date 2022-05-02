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
    // ICollection<Player> ListUsers();
    ICollection<Player> ListUsersInRoom(string roomId);
    GameRoom? RemovePlayer(string roomId, string playerId);
    GameRoom RemoveAllPlayers(string roomId);
    bool PlayerNameExists(string roomId, string playerName);
    bool PlayerIdExists(string roomId, string playerId);
    bool RoomNameExists(string roomName);
    bool RoomIdExists(string roomId);
    void DeleteAllRooms();
    void DeleteRoom(string roomId);
    GameRoom Vote(string roomId, string playerId, PlayerVote vote);
    GameRoom ClearVotes(string roomId);
}