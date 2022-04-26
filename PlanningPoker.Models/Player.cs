namespace PlanningPoker.Models;

public class Player
{
    public string Id { get; init; }
    public string Name { get; init; }
    
    public Player(string name)
    {
        Id = Guid.NewGuid().ToString("N").Replace("-", string.Empty).Substring(2, 10);
        Name = name;
    }
}