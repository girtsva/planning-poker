using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace PlanningPoker.ApiModels.Response;

[UsedImplicitly]
public class PlayerResponse
{
    //[JsonPropertyName("Id")]
    public string? Id { get; init; }
    public string? Name { get; init; }
}