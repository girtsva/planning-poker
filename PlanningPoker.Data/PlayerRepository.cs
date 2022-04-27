using PlanningPoker.Data.Interfaces;
using PlanningPoker.Models;

namespace PlanningPoker.Data;

public class PlayerRepository : IPlayerRepository
{
    private static readonly IDictionary<string, Player> Players = new Dictionary<string, Player>();

    public Player CreatePlayer(string playerName)
    {
        var player = new Player(playerName);
        Players.Add(player.Id, player);
        return player;
    }

    public ICollection<Player> ListPlayers()
    {
        return Players.Values;
    }

    public Player? GetPlayerById(string playerId)
    {
        return Players.ContainsKey(playerId) ? Players[playerId] : null;
    }
    
    public bool PlayerNameExists(string playerName)
    {
        return Players.Values.Any(player => player.Name == playerName);
    }
    
    public bool PlayerIdExists(string playerId)
    {
        return Players.ContainsKey(playerId);
    }
}