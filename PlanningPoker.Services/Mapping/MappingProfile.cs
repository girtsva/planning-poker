using AutoMapper;
using PlanningPoker.ApiModels.Response;
using PlanningPoker.Models;

namespace PlanningPoker.Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GameRoom, GameRoomResponse>()
            .ForMember(dest => dest.Id, opt 
                => opt.MapFrom(gameRoom => gameRoom.ExternalId));;
        CreateMap<Player, PlayerResponse>()
            .ForMember(dest => dest.Id, opt 
                => opt.MapFrom(player => player.ExternalId));
        CreateMap<PlayerVote, PlayerVoteResponse>();
    }
}