using AutoMapper;
using Microsoft.Extensions.Logging;
using PlanningPoker.ApiModels.Response;
using PlanningPoker.Common.Models;
using PlanningPoker.Data.Interfaces;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Services;

public class PlayerService : IPlayerService
{
    private readonly IDataRepository _dataRepository;
    private readonly ILogger<GameRoomService> _logger;
    private readonly IMapper _mapper;

    public PlayerService(IDataRepository dataRepository, ILogger<GameRoomService> logger, IMapper mapper)
    {
        _dataRepository = dataRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public bool PlayerNameExists(string roomId, string playerName)
    {
        return _dataRepository.PlayerNameExists(roomId, playerName);
    }
    
    public bool PlayerIdExists(string roomId, string playerId)
    {
        return _dataRepository.PlayerIdExists(roomId, playerId);
    }

    public GameRoomResponse Vote(string roomId, string playerId, VotingCard vote)
    {
        var gameRoom = _dataRepository.Vote(roomId, playerId, vote);
        _logger.LogInformation("Storing vote [{Vote}] of player with id [{PlayerId}] in a room with id [{RoomId}]," +
                               "returning game room object [{@GameRoom}]", vote, playerId, roomId, gameRoom);

        var response = _mapper.Map<GameRoomResponse>(gameRoom);
        _logger.LogInformation("Receiving transformed room with id [{RoomId}], room response object [{@Room}]", roomId, response);
        
        return response;
    }
}