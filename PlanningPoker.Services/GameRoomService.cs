﻿using Microsoft.Extensions.Logging;
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

    public GameRoom? AddPlayer(string roomId, Player player)
    {
        return _dataRepository.AddPlayer(roomId, player);
    }

    public ICollection<Player> ListUsers()
    {
        throw new NotImplementedException();
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