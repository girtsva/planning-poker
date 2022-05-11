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
        return _dbContext.GameRooms
            .AsNoTracking()
            .Include(gameRoom => gameRoom.Players)
            .Include(gameRoom => gameRoom.Votes)
            .ToList();
    }

    public GameRoom? GetGameRoomById(string roomId)
    {
        //return GameRooms.ContainsKey(roomId) ? GameRooms[roomId] : null;
        return _dbContext.GameRooms
            .AsNoTracking()
            .Include(gameRoom => gameRoom.Players)
            .Include(gameRoom => gameRoom.Votes)
            .FirstOrDefault(gameRoom => gameRoom.ExternalId == roomId);
    }

    // public GameRoom? AddPlayer(string roomId, Player player)
    // {
    //     var gameRoom = GameRooms[roomId];
    //     gameRoom?.Players.Add(player);
    //     return gameRoom;
    // }
    
    public GameRoom AddPlayer(string roomId, string playerName)
    {
        //var gameRoom = GameRooms[roomId];
        var gameRoom = _dbContext.GameRooms.First(gameRoom => gameRoom.ExternalId == roomId);
        gameRoom.Players.Add(new Player(playerName));
        _dbContext.SaveChanges();
        
        return gameRoom;
    }

    // public ICollection<Player> ListUsers()
    // {
    //     var players = GameRooms.Values.SelectMany(gameRoom => gameRoom.Players).ToList();
    //     return players;
    // }
    
    public ICollection<Player> ListPlayersInRoom(string roomId)
    {
        var gameRoom = _dbContext.GameRooms
            .Include(gameRoom => gameRoom.Players)
            .First(gameRoom => gameRoom.ExternalId == roomId);
        var players = gameRoom.Players.ToList();
        
        return players;
    }
    
    public GameRoom RemovePlayer(string roomId, string playerId)
    {
        var gameRoom = _dbContext.GameRooms
            .Include(gameRoom => gameRoom.Players)
            .First(gameRoom => gameRoom.ExternalId == roomId);
        var player = gameRoom.Players.First(player => player.ExternalId == playerId);
        //gameRoom.Players.Remove(player); // removes only FK GameRoomId from Players table; why it does not delete player from table?
        //_dbContext.Players.Remove(player);  // the same as below, right?
        _dbContext.Remove(player);
        _dbContext.SaveChanges();
        
        return gameRoom;
    }

    public GameRoom RemoveAllPlayers(string roomId)
    {
        var gameRoom = _dbContext.GameRooms
            .Include(gameRoom => gameRoom.Players)
            .First(gameRoom => gameRoom.ExternalId == roomId);
        _dbContext.RemoveRange(gameRoom.Players);
        _dbContext.SaveChanges();

        return gameRoom;
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
        return _dbContext.GameRooms.Any(gameRoom => gameRoom.Name == roomName);
    }
    
    public bool RoomIdExists(string roomId)
    {
        // return GameRooms.ContainsKey(roomId);
        return _dbContext.GameRooms.Any(gameRoom => gameRoom.ExternalId == roomId);
    }

    public void DeleteAllRooms()
    {
        //GameRooms.Clear();
        _dbContext.RemoveRange(_dbContext.Players); // without these two there is internal error 500
        _dbContext.RemoveRange(_dbContext.PlayerVotes);  // without these two there is internal error 500
        _dbContext.RemoveRange(_dbContext.GameRooms);
        _dbContext.SaveChanges();
    }

    public void DeleteRoom(string roomId)
    {
        //GameRooms.Remove(roomId);
        var gameRoom = _dbContext.GameRooms.First(gameRoom => gameRoom.ExternalId == roomId);
        // as alternative to below, RemoveAllPlayers and ClearVotes could be called
        var players = _dbContext.Players.Where(player => EF.Property<int>(player, "GameRoomId") == gameRoom.Id);
        foreach (var player in players)
        {
            _dbContext.Remove(player);  // if gameRoom.Players.Remove(player) <-- only FK GameRoomId from Players table removed
        }

        var votes = _dbContext.PlayerVotes.Where(vote => EF.Property<int>(vote, "GameRoomId") == gameRoom.Id);
        foreach (var vote in votes)
        {
            _dbContext.Remove(vote);
        }
        
        _dbContext.GameRooms.Remove(gameRoom);
        _dbContext.SaveChanges();
    }

    public GameRoom Vote(string roomId, string playerId, VotingCard vote)
    {
        var gameRoom = _dbContext.GameRooms.First(gameRoom => gameRoom.ExternalId == roomId);
        //gameRoom.Votes[playerId] = vote;
        gameRoom.Votes.Add(new PlayerVote(playerId, vote));
        _dbContext.SaveChanges();

        return gameRoom;
    }

    public GameRoom ClearVotes(string roomId)
    {
        var gameRoom = _dbContext.GameRooms
            .Include(gameRoom => gameRoom.Votes)
            .First(gameRoom => gameRoom.ExternalId == roomId);
        _dbContext.RemoveRange(gameRoom.Votes);
        _dbContext.SaveChanges();

        return gameRoom;
    }
}