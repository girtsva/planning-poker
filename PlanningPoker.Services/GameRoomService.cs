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
    
    public GameRoom? GetGameRoomByName(string roomName)
    {
        var gameRoom = _dataRepository.GetGameRoomByName(roomName);
        _logger.LogInformation("Receiving room [{RoomName}], room object [{@Room}]", roomName, gameRoom);
        return gameRoom;
    }

    // public void AddPlayer(Player name)
    // {
    //     _gameRoom.Players.Add(name);
    // }

    public ICollection<Player> ListUsers()
    {
        throw new NotImplementedException();
    }

    public bool RoomNameExists(string roomName)
    {
        return _dataRepository.RoomNameExists(roomName);
    }

    public void DeleteAllRooms()
    {
        _logger.LogInformation("Deleting all game rooms");
        _dataRepository.DeleteAllRooms();
    }

    public void DeleteRoom(string roomName)
    {
        _logger.LogInformation("Deleting room [{roomName}]", roomName);
        _dataRepository.DeleteRoom(roomName);
    }
}