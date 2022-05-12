using PlanningPoker.Common.Models;

namespace PlanningPoker.Models;

public class PlayerVote
{
    public int Id { get; init; }
    public string PlayerId { get; set; }
    public VotingCard Value { get; set; }
    
    public PlayerVote(string playerId, VotingCard value)
    {
        PlayerId = playerId;
        Value = value;
    }
}