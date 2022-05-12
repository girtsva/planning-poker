using AutoMapper;
using PlanningPoker.ApiModels.Response;
using PlanningPoker.Models;

namespace PlanningPoker.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GameRoom, GameRoomResponse>();
        CreateMap<Player, PlayerResponse>();
        CreateMap<PlayerVote, PlayerVoteResponse>();
    }
}