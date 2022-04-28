using Microsoft.Extensions.Logging;
using PlanningPoker.Data.Interfaces;
using PlanningPoker.Models;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Services;

public class GameRoomService : IGameRoomService
{
    //private readonly GameRoom _gameRoom;
    private readonly IDataRepository _dataRepository;
    private readonly ILogger<GameRoomService> _logger;

    public GameRoomService(IDataRepository dataRepository, ILogger<GameRoomService> logger)
    {
        _dataRepository = dataRepository;
        _logger = logger;
        //_gameRoom = gameRoom;
    }

    public GameRoom CreateGameRoom(string roomName)
    {
        var gameRoom = _dataRepository.CreateGameRoom(roomName);
        _logger.LogInformation("Creating room [{RoomName}], room object [{@Room}]", roomName, gameRoom);
        return gameRoom;
    }

    public ICollection<GameRoom> ListGameRooms()
    {
        var gameRooms = _dataRepository.ListGameRooms();
        _logger.LogInformation("Receiving room objects [{@Rooms}]", gameRooms);
        return gameRooms;
    }

    public GameRoom? GetGameRoomById(string roomId)
    {
        var gameRoom = _dataRepository.GetGameRoomById(roomId);
        _logger.LogInformation("Receiving room with id [{RoomId}], room object [{@Room}]", roomId, gameRoom);
        return gameRoom;
    }

    // public GameRoom? AddPlayer(string roomId, Player player)
    // {
    //     return _dataRepository.AddPlayer(roomId, player);
    // }
    
    public GameRoom? AddPlayer(string roomId, string playerName)
    {
        var gameRoom = _dataRepository.AddPlayer(roomId, playerName);
        _logger.LogInformation("Adding player [{PlayerName}] to room id [{RoomId}], receiving room object [{@Room}]", playerName, roomId, gameRoom);
        return gameRoom;
    }

    public ICollection<Player> ListUsersInRoom(string roomId)
    {
        var players = _dataRepository.ListUsersInRoom(roomId);
        _logger.LogInformation("Receiving player objects [{@Players}] for room id [{RoomId}]", players, roomId);
        return players;
    }

    public GameRoom? RemovePlayer(string roomId, string playerId)
    {
        var gameRoom = _dataRepository.RemovePlayer(roomId, playerId);
        _logger.LogInformation("Removing player with id [{PlayerId}] from room id [{RoomId}], receiving room object [{@Room}]", playerId, roomId, gameRoom);
        return gameRoom;
    }

    public bool RoomNameExists(string roomName)
    {
        return _dataRepository.RoomNameExists(roomName);
    }
    
    public bool RoomIdExists(string roomId)
    {
        return _dataRepository.RoomIdExists(roomId);
    }

    public void DeleteAllRooms()
    {
        _logger.LogInformation("Deleting all game rooms");
        _dataRepository.DeleteAllRooms();
    }

    public void DeleteRoom(string roomId)
    {
        _logger.LogInformation("Deleting room with id [{roomName}]", roomId);
        _dataRepository.DeleteRoom(roomId);
    }
}