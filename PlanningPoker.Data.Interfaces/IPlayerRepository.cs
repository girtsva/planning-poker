using PlanningPoker.Models;

namespace PlanningPoker.Data.Interfaces;

public interface IPlayerRepository
{
    Player CreatePlayer(string playerName);
    ICollection<Player> ListPlayers();
    Player? GetPlayerByName(string playerName);
    bool PlayerNameExists(string playerName);
}