namespace PlanningPoker.Models;

public class GameRoom
{
    public int Id { get; set; } = 0;  // random 10 letters on room creating
    public string Name { get; set; }

    public ICollection<Player> Players { get; set; } = new List<Player>();
    //public int NumberOfPlayers { get; set; }

    public GameRoom(string name)
    {
        Id++;
        Name = name;
    }
}