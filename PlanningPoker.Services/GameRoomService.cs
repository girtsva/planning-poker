using Microsoft.Extensions.Logging;
using PlanningPoker.Data.Interfaces;
using PlanningPoker.Models;
using PlanningPoker.Services.Interfaces;
using Serilog;

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
        return _dataRepository.ListGameRooms();
    }
    
    public GameRoom? GetGameRoomByName(string roomName)
    {
        return _dataRepository.GetGameRoomByName(roomName);
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
        _dataRepository.DeleteAllRooms();
    }

    public void DeleteRoom(string roomName)
    {
        _dataRepository.DeleteRoom(roomName);
    }
}