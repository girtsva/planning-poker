using JetBrains.Annotations;
using PlanningPoker.Common.Models;

namespace PlanningPoker.ApiModels.Response;

[UsedImplicitly]
public class PlayerVoteResponse
{
    public string? PlayerId { get; set; }
    public VotingCard Value { get; set; }
}