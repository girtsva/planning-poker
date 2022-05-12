using Microsoft.EntityFrameworkCore;
using PlanningPoker.Data.Interfaces;
using PlanningPoker.Models;

namespace PlanningPoker.Data;

//[UsedImplicitly]
public class DataRepository : IDataRepository
{
    private readonly PlanningPokerDbContext _dbContext;

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
        return _dbContext.GameRooms
            .AsNoTracking()
            .Include(gameRoom => gameRoom.Players)
            .Include(gameRoom => gameRoom.Votes)
            .FirstOrDefault(gameRoom => gameRoom.ExternalId == roomId);
    }
    
    public GameRoom AddPlayer(string roomId, string playerName)
    {
        var gameRoom = _dbContext.GameRooms.First(gameRoom => gameRoom.ExternalId == roomId);
        gameRoom.Players.Add(new Player(playerName));
        _dbContext.SaveChanges();
        
        return gameRoom;
    }

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
        return _dbContext.GameRooms.Any(gameRoom => gameRoom.Name == roomName);
    }
    
    public bool RoomIdExists(string roomId)
    {
        return _dbContext.GameRooms.Any(gameRoom => gameRoom.ExternalId == roomId);
    }

    public bool VoteExists(string roomId, string playerId)
    {
        var gameRoom = GetGameRoomById(roomId);
        return gameRoom!.Votes.Any(vote => vote.PlayerId == playerId);
    }

    public void DeleteAllRooms()
    {
        _dbContext.RemoveRange(_dbContext.Players);
        _dbContext.RemoveRange(_dbContext.PlayerVotes);
        _dbContext.RemoveRange(_dbContext.GameRooms);
        _dbContext.SaveChanges();
    }

    public void DeleteRoom(string roomId)
    {
        var gameRoom = _dbContext.GameRooms.First(gameRoom => gameRoom.ExternalId == roomId);
        
        RemoveAllPlayers(roomId);
        ClearVotes(roomId);
        
        _dbContext.GameRooms.Remove(gameRoom);
        _dbContext.SaveChanges();
    }

    public GameRoom Vote(string roomId, string playerId, VotingCard vote)
    {
        var gameRoom = _dbContext.GameRooms
            .Include(gameRoom => gameRoom.Votes)
            .First(gameRoom => gameRoom.ExternalId == roomId);
        
        if (VoteExists(roomId, playerId))
        {
            var existingVote = gameRoom.Votes.First(playerVote => playerVote.PlayerId == playerId);
            existingVote.Value = vote;
        }
        else
        {
            gameRoom.Votes.Add(new PlayerVote(playerId, vote));
        }
        
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