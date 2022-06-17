using Microsoft.EntityFrameworkCore;
using PlanningPoker.Models;

namespace PlanningPoker.Data;

public class PlanningPokerDbContext : DbContext
{
    //private string? ConnectionString { get; }
    public PlanningPokerDbContext(DbContextOptions<PlanningPokerDbContext> options) : base(options) { }

    // public PlanningPokerDbContext(string connectionString)
    // {
    //     ConnectionString = connectionString;
    // }
    
    public DbSet<GameRoom> GameRooms { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerVote> PlayerVotes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

        // if (ConnectionString is not null)
        // {
        //     optionsBuilder
        //         .UseSqlServer(ConnectionString);
        // }
            
    }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<GameRoom>()
    //         .Property(gr => gr.Name)
    //         .IsRequired();
    // }
}