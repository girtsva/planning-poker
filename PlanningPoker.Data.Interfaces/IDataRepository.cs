using PlanningPoker.Common.Models;
using PlanningPoker.Models;

namespace PlanningPoker.Data.Interfaces;

public interface IDataRepository
{
    /// <summary>
    ///     Creates a new game room in database.
    /// </summary>
    /// <param name="roomName">The desired name of the game room</param>
    /// <returns>Instance of created game room</returns>
    GameRoom CreateGameRoom(string roomName);

    /// <summary>
    ///     Returns a list of created game rooms in database.
    /// </summary>
    /// <returns>List of created game rooms (AsNoTracking) if found; otherwise empty list</returns>
    ICollection<GameRoom> ListGameRooms();

    /// <summary>
    ///     Searches and returns game room from data repository by given room id.
    /// </summary>
    /// <param name="roomId">Specified game room Id</param>
    /// <returns>Instance of found game room (AsNoTracking) if found; otherwise <c>null</c></returns>
    GameRoom? GetGameRoomById(string roomId);
    // GameRoom? AddPlayer(string roomId, Player player);
    
    /// <summary>
    ///     Adds player to the given game room by room id.
    /// </summary>
    /// <param name="roomId">Specified game room Id</param>
    /// <param name="playerName">Specified player name</param>
    /// <returns>Instance of updated game room.</returns>
    GameRoom AddPlayer(string roomId, string playerName);
    
    /// <summary>
    ///     Lists players in the game room.
    /// </summary>
    /// <returns>List of players if any; otherwise empty list.</returns>
    ICollection<Player> ListPlayersInRoom(string roomId);
    
    /// <summary>
    ///     Removes player from the given game room by id.
    /// </summary>
    /// <param name="roomId">Specified game room Id</param>
    /// <param name="playerId">Specified player Id</param>
    /// <returns>Instance of updated game room.</returns>
    GameRoom RemovePlayer(string roomId, string playerId);
    
    /// <summary>
    ///     Removes all players from the given game room by id.
    /// </summary>
    /// <param name="roomId">Specified game room Id</param>
    /// <returns>Instance of updated game room.</returns>
    GameRoom RemoveAllPlayers(string roomId);
    
    /// <summary>
    ///     Checks whether player with specified name exists in the database.
    /// </summary>
    /// <param name="roomId">Specified room Id.</param>
    /// <param name="playerName">Specified player name.</param>
    /// <returns><c>true</c> if player exists; otherwise <c>false</c>.</returns>
    bool PlayerNameExists(string roomId, string playerName);
    
    /// <summary>
    ///     Checks whether player with specified id exists in the database.
    /// </summary>
    /// <param name="roomId">Specified room Id.</param>
    /// <param name="playerId">Specified player Id.</param>
    /// <returns><c>true</c> if player exists; otherwise <c>false</c>.</returns>
    bool PlayerIdExists(string roomId, string playerId);
    
    /// <summary>
    ///     Checks whether game room with specified name exists in the database.
    /// </summary>
    /// <param name="roomName">Specified game room name</param>
    /// <returns><c>true</c> if game room exists; otherwise <c>false</c></returns>
    bool RoomNameExists(string roomName);
    
    /// <summary>
    ///     Checks whether game room with specified id exists in the database.
    /// </summary>
    /// <param name="roomId">Specified game room Id</param>
    /// <returns><c>true</c> if game room exists; otherwise <c>false</c>.</returns>
    bool RoomIdExists(string roomId);
    
    /// <summary>
    ///     Checks whether vote of the specified player in the specified game room exists in the database.
    /// </summary>
    /// <param name="roomId">Specified game room Id</param>
    /// <param name="playerId">The Id of player who has voted</param>
    /// <returns><c>true</c> if vote exists; otherwise <c>false</c>.</returns>
    bool VoteExists(string roomId, string playerId);
    
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
    ///     Submits the vote of the specified player in the specified game room.
    /// </summary>
    /// <param name="roomId">The id of game room in which the voting process is happening</param>
    /// <param name="playerId">The id of player who has voted</param>
    /// <param name="vote">The vote value chosen</param>
    /// <returns>Updated instance of game room.</returns>
    GameRoom Vote(string roomId, string playerId, VotingCard vote);
    
    /// <summary>
    ///     Clears user votes in the specified game room.
    /// </summary>
    /// <param name="roomId">Specified game room Id</param>
    /// <returns>Instance of updated game room.</returns>
    GameRoom ClearVotes(string roomId);
}