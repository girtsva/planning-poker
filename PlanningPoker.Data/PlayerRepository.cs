using PlanningPoker.Data.Interfaces;
using PlanningPoker.Models;

namespace PlanningPoker.Data;

public class PlayerRepository : IPlayerRepository
{
    private static readonly IDictionary<string, Player> Players = new Dictionary<string, Player>();

    public Player CreatePlayer(string playerName)
    {
        var player = new Player(playerName);
        Players.Add(playerName, player);
        return player;
    }

    public ICollection<Player> ListPlayers()
    {
        return Players.Values;
    }

    public Player? GetPlayerByName(string playerName)
    {
        return Players.ContainsKey(playerName) ? Players[playerName] : null;
    }
    
    public bool PlayerNameExists(string playerName)
    {
        return Players.ContainsKey(playerName);
    }
}