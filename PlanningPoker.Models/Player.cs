using System.Text.Json.Serialization;

namespace PlanningPoker.Models;

public class Player
{
    [JsonIgnore]
    public int Id { get; init; }
    public string ExternalId { get; init; }
    public string Name { get; init; }
    
    public Player(string name)
    {
        ExternalId = Guid.NewGuid().ToString("N").Replace("-", string.Empty).Substring(2, 10);
        Name = name;
    }
}