using AutoMapper;
using Microsoft.Extensions.Logging;
using PlanningPoker.ApiModels.Response;
using PlanningPoker.Common.Models;
using PlanningPoker.Data.Interfaces;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Services;

public class GameRoomService : IGameRoomService
{
    private readonly IDataRepository _dataRepository;
    private readonly ILogger<GameRoomService> _logger;
    private readonly IMapper _mapper;

    public GameRoomService(IDataRepository dataRepository, ILogger<GameRoomService> logger, IMapper mapper)
    {
        _dataRepository = dataRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public GameRoomResponse CreateGameRoom(string roomName)
    {
        var gameRoom = _dataRepository.CreateGameRoom(roomName);
        _logger.LogDebug("Creating room [{RoomName}], room object [{@Room}]", roomName, gameRoom);
        
        var response = _mapper.Map<GameRoomResponse>(gameRoom);
        _logger.LogDebug("Receiving transformed room with name [{RoomName}], room response object [{@Room}]", roomName, response);
        
        return response;
    }

    public ICollection<GameRoomResponse> ListGameRooms()
    {
        var gameRooms = _dataRepository.ListGameRooms();
        _logger.LogDebug("Receiving room objects [{@Rooms}]", gameRooms);

        //var response = gameRooms.Select(gameRoom => _mapper.Map<GameRoomResponse>(gameRoom)).ToList();
        var response = _mapper.Map<ICollection<GameRoomResponse>>(gameRooms);
        _logger.LogDebug("Receiving transformed game room response objects [{@GameRooms}]", response);
        
        return response;
    }

    public GameRoomResponse? GetGameRoomById(string roomId)
    {
        var gameRoom = _dataRepository.GetGameRoomById(roomId);
        _logger.LogDebug("Receiving room with id [{RoomId}], room object [{@Room}]", roomId, gameRoom);
        
        var response = _mapper.Map<GameRoomResponse>(gameRoom);
        _logger.LogDebug("Receiving transformed room with id [{RoomId}], room response object [{@Room}]", roomId, response);
        
        return response;
    }

    public GameRoomResponse? AddPlayer(string roomId, string playerName)
    {
        var gameRoom = _dataRepository.AddPlayer(roomId, playerName);
        _logger.LogDebug("Adding player [{PlayerName}] to room id [{RoomId}], receiving room object [{@Room}]", playerName, roomId, gameRoom);

        var response = _mapper.Map<GameRoomResponse>(gameRoom);
        _logger.LogDebug("Receiving transformed room with id [{RoomId}], room response object [{@Room}]", roomId, response);
        
        return response;
    }

    public ICollection<PlayerResponse> ListPlayersInRoom(string roomId)
    {
        var players = _dataRepository.ListPlayersInRoom(roomId);
        _logger.LogDebug("Receiving player objects [{@Players}] for room id [{RoomId}]", players, roomId);

        //var response = players.Select(player => _mapper.Map<PlayerResponse>(player)).ToList();
        var response = _mapper.Map<ICollection<PlayerResponse>>(players);
        _logger.LogDebug("Receiving transformed player response objects [{@Players}] for room id [{RoomId}]", response, roomId);
        
        return response;
    }

    public GameRoomResponse? RemovePlayer(string roomId, string playerId)
    {
        var gameRoom = _dataRepository.RemovePlayer(roomId, playerId);
        _logger.LogDebug("Removing player with id [{PlayerId}] from room id [{RoomId}], receiving room object [{@Room}]", playerId, roomId, gameRoom);

        var response = _mapper.Map<GameRoomResponse>(gameRoom);
        _logger.LogDebug("Receiving transformed room with id [{RoomId}], room response object [{@Room}]", roomId, response);
        
        return response;
    }

    public GameRoomResponse RemoveAllPlayers(string roomId)
    {
        var gameRoom = _dataRepository.RemoveAllPlayers(roomId);
        _logger.LogDebug("Removing all players from room id [{RoomId}], receiving room object [{@Room}]", roomId, gameRoom);

        var response = _mapper.Map<GameRoomResponse>(gameRoom);
        _logger.LogDebug("Receiving transformed room with id [{RoomId}], room response object [{@Room}]", roomId, response);
        
        return response;
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
        _logger.LogInformation("Deleting room with id [{RoomName}]", roomId);
        _dataRepository.DeleteRoom(roomId);
    }

    public Array ShowVotingCards()
    {
        return Enum.GetValues(typeof(VotingCard));
    }

    public GameRoomResponse ClearVotes(string roomId)
    {
        _logger.LogInformation("Clearing votes in room with id [{RoomName}]", roomId);
        var gameRoom = _dataRepository.ClearVotes(roomId);
        
        var response = _mapper.Map<GameRoomResponse>(gameRoom);
        _logger.LogDebug("Receiving transformed room with id [{RoomId}], room response object [{@Room}]", roomId, response);
        
        return response;
    }
}