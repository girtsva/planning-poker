namespace PlanningPoker.Models;

public class VotingSystem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<string> Values { get; set; }
}