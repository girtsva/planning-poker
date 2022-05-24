using PlanningPoker.ApiModels.Response;
using PlanningPoker.Common.Models;

namespace PlanningPoker.Services.Interfaces;

public interface IPlayerService
{
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
    ///     Submits the vote of the specified player in the specified game room.
    /// </summary>
    /// <param name="roomId">The id of game room in which the voting process is happening</param>
    /// <param name="playerId">The id of player who has voted</param>
    /// <param name="vote">The vote value chosen</param>
    /// <returns>Response object of updated game room.</returns>
    GameRoomResponse Vote(string roomId, string playerId, VotingCard vote);
}