using Microsoft.EntityFrameworkCore;
using PlanningPoker.Data.Interfaces;
using PlanningPoker.Models;

namespace PlanningPoker.Data;

//[UsedImplicitly]
public class DataRepository : IDataRepository
{
    private readonly PlanningPokerDbContext _dbContext;

    //public static ICollection<Player> Players = new List<Player>();
    //private static readonly IDictionary<string, GameRoom> GameRooms = new Dictionary<string, GameRoom>();

    public DataRepository(PlanningPokerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public GameRoom CreateGameRoom(string roomName)
    {
        var gameRoom = new GameRoom(roomName);
        _dbContext.GameRooms.Add(gameRoom);
        _dbContext.SaveChanges();
        return gameRoom;
    }

    public ICollection<GameRoom> ListGameRooms()
    {
        return null; //GameRooms.Values;
    }

    public GameRoom? GetGameRoomById(string roomId)
    {
        return null; // GameRooms.ContainsKey(roomId) ? GameRooms[roomId] : null;
    }

    // public GameRoom? AddPlayer(string roomId, Player player)
    // {
    //     var gameRoom = GameRooms[roomId];
    //     gameRoom?.Players.Add(player);
    //     return gameRoom;
    // }
    
    public GameRoom AddPlayer(string roomId, string playerName)
    {
        // var gameRoom = GameRooms[roomId];
        // gameRoom.Players.Add(new Player(playerName));
        // return gameRoom;
        return null;
    }

    // public ICollection<Player> ListUsers()
    // {
    //     var players = GameRooms.Values.SelectMany(gameRoom => gameRoom.Players).ToList();
    //     return players;
    // }
    
    public ICollection<Player> ListPlayersInRoom(string roomId)
    {
        // var players = GameRooms[roomId].Players.ToList();
        // return players;
        return null;
    }
    
    public GameRoom RemovePlayer(string roomId, string playerId)
    {
        // var gameRoom = GameRooms[roomId];
        // var player = gameRoom.Players.First(player => player.ExternalId == playerId);
        // gameRoom.Players.Remove(player);
        // return gameRoom;
        return null;
    }

    public GameRoom RemoveAllPlayers(string roomId)
    {
        // var gameRoom = GameRooms[roomId];
        // gameRoom.Players.Clear();
        //
        // return gameRoom;
        return null;
    }
    
    public bool PlayerNameExists(string roomId, string playerName)
    {
        var players = ListPlayersInRoom(roomId);
        
        return players.Any(player => player.Name == playerName);
    }
    
    public bool PlayerIdExists(string roomId, string playerId)
    {
        var players = ListPlayersInRoom(roomId);
        
        return players.Any(player => player.ExternalId == playerId);
    }

    public bool RoomNameExists(string roomName)
    {
        //return GameRooms.Values.Any(gameRoom => gameRoom.Name == roomName);
        return false;
    }
    
    public bool RoomIdExists(string roomId)
    {
        // return GameRooms.ContainsKey(roomId);
        return false;
    }

    public void DeleteAllRooms()
    {
        //GameRooms.Clear();
    }

    public void DeleteRoom(string roomId)
    {
        //GameRooms.Remove(roomId);
    }

    public GameRoom Vote(string roomId, string playerId, VotingCard vote)
    {
        // var gameRoom = GameRooms[roomId];
        // //gameRoom.Votes[playerId] = vote;
        // gameRoom.Votes.Add(new PlayerVote(playerId, vote));
        //
        // return gameRoom;
        return null;
    }

    public GameRoom ClearVotes(string roomId)
    {
        // var gameRoom = GameRooms[roomId];
        // gameRoom.Votes.Clear();
        //
        // return gameRoom;
        return null;
    }
}