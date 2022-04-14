namespace PlanningPoker.Models;

public class GameRoom
{
    public int Id { get; set; } = 0;
    public string Name { get; set; }
    public ICollection<Player> Players { get; set; }
    //public int NumberOfPlayers { get; set; }

    public GameRoom(string name)
    {
        Id++;
        Name = name;
    }
}