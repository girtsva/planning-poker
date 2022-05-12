using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json.Serialization;

namespace PlanningPoker.Models;

public class GameRoom
{
    [JsonIgnore]
    public int Id { get; init; }
    public string ExternalId { get; init; }   // random 10 letters on room creating
    public string Name { get; set; }

    public ICollection<Player> Players { get; set; } = new List<Player>();
    //public IDictionary<string, VotingCard> Votes { get; set; } = new Dictionary<string, VotingCard>();
    public ICollection<PlayerVote> Votes { get; set; } = new List<PlayerVote>();

    public GameRoom(string name)
    {
        ExternalId = GenerateRandomId();
        Name = name;
    }

    private static string GenerateRandomId()
    {
        StringBuilder builder = new StringBuilder();
        Enumerable
            .Range(65, 26)
            .Select(e => ((char)e).ToString())
            .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
            .OrderBy(e => Guid.NewGuid())
            .Take(10)
            .ToList().ForEach(e => builder.Append(e));
        return builder.ToString();
    }
}