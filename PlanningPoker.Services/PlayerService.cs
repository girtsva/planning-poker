using Microsoft.Extensions.Logging;
using PlanningPoker.Common.Models;
using PlanningPoker.Data.Interfaces;
using PlanningPoker.Models;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Services;

public class PlayerService : IPlayerService
{
    //private readonly GameRoom _gameRoom;
    //private readonly IPlayerRepository _playerRepository;
    private readonly IDataRepository _dataRepository;
    private readonly ILogger<GameRoomService> _logger;

    public PlayerService(IDataRepository dataRepository, ILogger<GameRoomService> logger) //IPlayerRepository playerRepository
    {
        //_playerRepository = playerRepository;
        _dataRepository = dataRepository;
        _logger = logger;
    }

    // public Player CreatePlayer(string playerName)
    // {
    //     var player = _playerRepository.CreatePlayer(playerName);
    //     _logger.LogInformation("Creating player [{PlayerName}], player object [{@Player}]", playerName, player);
    //     return player;
    // }

    // public ICollection<Player> ListPlayers()
    // {
    //     var players = _playerRepository.ListPlayers();
    //     _logger.LogInformation("Receiving player objects [{@Players}]", players);
    //     return players;
    // }

    // public Player? GetPlayerById(string playerId)
    // {
    //     var player = _playerRepository.GetPlayerById(playerId);
    //     _logger.LogInformation("Receiving player with id [{PlayerId}], player object [{@Player}]", playerId, player);
    //     return player;
    // }
    
    // public bool PlayerNameExists(string playerName)
    // {
    //     return _playerRepository.PlayerNameExists(playerName);
    // }
    //
    // public bool PlayerIdExists(string playerId)
    // {
    //     return _playerRepository.PlayerIdExists(playerId);
    // }

    public bool PlayerNameExists(string roomId, string playerName)
    {
        return _dataRepository.PlayerNameExists(roomId, playerName);
    }
    
    public bool PlayerIdExists(string roomId, string playerId)
    {
        return _dataRepository.PlayerIdExists(roomId, playerId);
    }

    public GameRoom Vote(string roomId, string playerId, VotingCard vote)
    {
        var gameRoom = _dataRepository.Vote(roomId, playerId, vote);
        _logger.LogInformation("Storing vote [{Vote}] of player with id [{PlayerId}] in a room with id [{RoomId}]," +
                               "returning game room object [{@GameRoom}]", vote, playerId, roomId, gameRoom);
        return gameRoom;
    }
}