using PlanningPoker.Models;

namespace PlanningPoker.Data.Interfaces;

public interface IPlayerRepository
{
    Player CreatePlayer(string playerName);
    ICollection<Player> ListPlayers();
    Player? GetPlayerById(string playerId);
    bool PlayerNameExists(string playerName);
    bool PlayerIdExists(string playerId);
}