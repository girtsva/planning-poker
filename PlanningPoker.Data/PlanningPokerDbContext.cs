using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PlanningPoker.Models;

namespace PlanningPoker.Data;

[SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Global")]
public class PlanningPokerDbContext : DbContext
{
    //private string? ConnectionString { get; }
    public PlanningPokerDbContext(DbContextOptions<PlanningPokerDbContext> options) : base(options) { }

    // public PlanningPokerDbContext(string connectionString)
    // {
    //     ConnectionString = connectionString;
    // }
    
    public DbSet<GameRoom> GameRooms => Set<GameRoom>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<PlayerVote> PlayerVotes => Set<PlayerVote>();

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