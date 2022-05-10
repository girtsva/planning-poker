using PlanningPoker.Models;

namespace PlanningPoker.Services.Interfaces;

public interface IPlayerService
{
    // /// <summary>
    // ///     Creates a new player in the player repository.
    // /// </summary>
    // /// <param name="playerName">The desired player name.</param>
    // /// <returns>Created player object.</returns>
    // Player CreatePlayer(string playerName);
    //
    // /// <summary>
    // ///     Returns a list of created players in the player repository.
    // /// </summary>
    // /// <returns>List of created players if found; otherwise empty list.</returns>
    // ICollection<Player> ListPlayers();
    //
    // /// <summary>
    // ///     Searches and returns player from player repository by given player id.
    // /// </summary>
    // /// <param name="playerId">Specified player id.</param>
    // /// <returns>Instance of player if found; otherwise <c>null</c>.</returns>
    // Player? GetPlayerById(string playerId);
    //
    // /// <summary>
    // ///     Checks whether player with specified name exists in the player repository.
    // /// </summary>
    // /// <param name="playerName">Specified player name.</param>
    // /// <returns><c>true</c> if player exists; otherwise <c>false</c>.</returns>
    // bool PlayerNameExists(string playerName);
    //
    // /// <summary>
    // ///     Checks whether player with specified id exists in the player repository.
    // /// </summary>
    // /// <param name="playerId">Specified player id.</param>
    // /// <returns><c>true</c> if player exists; otherwise <c>false</c>.</returns>
    // bool PlayerIdExists(string playerId);

    /// <summary>
    ///     Checks whether player with specified name exists in the data repository.
    /// </summary>
    /// <param name="roomId">Specified room id.</param>
    /// <param name="playerName">Specified player name.</param>
    /// <returns><c>true</c> if player exists; otherwise <c>false</c>.</returns>
    bool PlayerNameExists(string roomId, string playerName);

    /// <summary>
    ///     Checks whether player with specified id exists in the data repository.
    /// </summary>
    /// <param name="roomId">Specified room id.</param>
    /// <param name="playerId">Specified player id.</param>
    /// <returns><c>true</c> if player exists; otherwise <c>false</c>.</returns>
    bool PlayerIdExists(string roomId, string playerId);

    /// <summary>
    ///     Submits the vote of the specified player in the specified game room.
    /// </summary>
    /// <param name="roomId">The id of game room in which the voting process is happening</param>
    /// <param name="playerId">The id of player who has voted</param>
    /// <param name="vote">The vote value chosen</param>
    /// <returns>Updated instance of game room.</returns>
    GameRoom Vote(string roomId, string playerId, VotingCard vote);
}