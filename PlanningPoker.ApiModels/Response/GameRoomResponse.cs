using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace PlanningPoker.ApiModels.Response;

[UsedImplicitly]
public class GameRoomResponse
{
    //[JsonPropertyName("Id")]
    public string? Id { get; set; }
    public string? Name { get; set; }

    public ICollection<PlayerResponse>? Players { get; set; }
    public ICollection<PlayerVoteResponse>? Votes { get; set; }
}