using PlanningPoker.Models;

namespace PlanningPoker.Services.Interfaces;

public interface IPlayerService
{
    /// <summary>
    ///     Creates a new player in the player repository.
    /// </summary>
    /// <param name="playerName">The desired player name.</param>
    /// <returns>Created player object.</returns>
    Player CreatePlayer(string playerName);
    
    /// <summary>
    ///     Returns a list of created players in the player repository.
    /// </summary>
    /// <returns>List of created players if found; otherwise empty list.</returns>
    ICollection<Player> ListPlayers();
    
    /// <summary>
    ///     Searches and returns player from player repository by given player name.
    /// </summary>
    /// <param name="playerName">Specified player name.</param>
    /// <returns>Instance of player if found; otherwise <c>null</c>.</returns>
    Player? GetPlayerByName(string playerName);
    
    /// <summary>
    ///     Checks whether player with specified name exists in the player repository.
    /// </summary>
    /// <param name="playerName">Specified player name.</param>
    /// <returns><c>true</c> if player exists; otherwise <c>false</c>.</returns>
    bool PlayerNameExists(string playerName);
}