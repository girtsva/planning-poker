using System.Text;

namespace PlanningPoker.Models;

public class GameRoom
{
    public string Id { get; init; }   // random 10 letters on room creating
    public string Name { get; set; }

    public ICollection<Player> Players { get; set; } = new List<Player>();
    //public int NumberOfPlayers { get; set; }

    public GameRoom(string name)
    {
        Id = GenerateRandomId();
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