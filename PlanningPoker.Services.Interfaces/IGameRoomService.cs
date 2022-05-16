using PlanningPoker.ApiModels.Response;
using PlanningPoker.Models;

namespace PlanningPoker.Services.Interfaces;

public interface IGameRoomService
{
    /// <summary>
    ///     Creates a new game room in the data repository.
    /// </summary>
    /// <param name="roomName">The desired name of the game room</param>
    GameRoomResponse CreateGameRoom(string roomName);

    /// <summary>
    ///     Returns a list of created game rooms in the data repository.
    /// </summary>
    /// <returns>List of created game rooms if found; otherwise empty list</returns>
    ICollection<GameRoomResponse> ListGameRooms();

    /// <summary>
    ///     Searches and returns game room from data repository by given room name.
    /// </summary>
    /// <param name="roomId">Specified game room Id</param>
    /// <returns>Instance of found game room if found; otherwise <c>null</c></returns>
    GameRoomResponse? GetGameRoomById(string roomId);

    // /// <summary>
    // ///     Adds player to the given game room by Id.
    // /// </summary>
    // /// <param name="roomId">Specified game room Id</param>
    // /// <param name="player">Specified player</param>
    // /// <returns>Instance of updated game room.</returns>
    // GameRoom? AddPlayer(string roomId, Player player);

    /// <summary>
    ///     Adds player to the given game room by Id.
    /// </summary>
    /// <param name="roomId">Specified game room Id</param>
    /// <param name="playerName">Specified player name</param>
    /// <returns>Instance of updated game room.</returns>
    GameRoomResponse? AddPlayer(string roomId, string playerName);
    
    /// <summary>
    ///     Lists players in the game room.
    /// </summary>
    /// <returns>List of players if any; otherwise empty list.</returns>
    ICollection<PlayerResponse> ListPlayersInRoom(string roomId);

    /// <summary>
    ///     Removes player from the given game room by id.
    /// </summary>
    /// <param name="roomId">Specified game room Id</param>
    /// <param name="playerId">Specified player Id</param>
    /// <returns>Instance of updated game room.</returns>
    GameRoomResponse? RemovePlayer(string roomId, string playerId);

    /// <summary>
    ///     Removes all players from the given game room by id.
    /// </summary>
    /// <param name="roomId">Specified game room Id</param>
    /// <returns>Instance of updated game room.</returns>
    GameRoomResponse RemoveAllPlayers(string roomId);

    /// <summary>
    ///     Checks whether game room with specified name exists in the data repository.
    /// </summary>
    /// <param name="roomName">Specified game room name</param>
    /// <returns><c>true</c> if game room exists; otherwise <c>false</c></returns>
    bool RoomNameExists(string roomName);

    /// <summary>
    ///     Checks whether game room with specified id exists in the data repository.
    /// </summary>
    /// <param name="roomId">Specified game room Id</param>
    /// <returns><c>true</c> if game room exists; otherwise <c>false</c>.</returns>
    bool RoomIdExists(string roomId);

    /// <summary>
    ///     Deletes all game rooms from the data repository.
    /// </summary>
    void DeleteAllRooms();

    /// <summary>
    ///     Deletes the specified game room by room id from the data repository.
    /// </summary>
    /// <param name="roomId">Specified game room id</param>
    void DeleteRoom(string roomId);

    /// <summary>
    ///     Shows the available values of the voting cards.
    /// </summary>
    Array ShowVotingCards();
    
    /// <summary>
    ///     Clears user votes in the specified game room.
    /// </summary>
    /// <param name="roomId">Specified game room id</param>
    /// <returns>Instance of updated game room.</returns>
    GameRoomResponse ClearVotes(string roomId);
    
    // /// <summary>
    // ///     Checks whether the provided player vote value exists in the defined enum.
    // /// </summary>
    // /// <param name="vote">Specified player vote value</param>
    // /// <returns><c>true</c> if vote value exists; otherwise <c>false</c></returns>
    // bool VoteExists(PlayerVote vote);
}