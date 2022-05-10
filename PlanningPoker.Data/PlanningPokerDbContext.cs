using Microsoft.EntityFrameworkCore;
using PlanningPoker.Models;

namespace PlanningPoker.Data;

public class PlanningPokerDbContext : DbContext
{
    public PlanningPokerDbContext(DbContextOptions<PlanningPokerDbContext> options) : base(options) { }
    
    public DbSet<GameRoom>? GameRooms { get; set; }
    public DbSet<Player>? Players { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder
    //         .EnableSensitiveDataLogging()
    //         .EnableDetailedErrors();
    // }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<GameRoom>()
    //         .Property(gr => gr.Name)
    //         .IsRequired();
    // }
}