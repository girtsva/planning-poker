using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlanningPoker.Data.Interfaces;

namespace PlanningPoker.Services;

public class GameRoomExpirationService: IHostedService, IDisposable
{
    private readonly ILogger<GameRoomExpirationService> _logger;
    private readonly IServiceProvider _services;
    private Timer _timer = null!;
    
    private const int DaysToKeepGameRoom = 5;

    public GameRoomExpirationService(ILogger<GameRoomExpirationService> logger, IServiceProvider services)
    {
        _logger = logger;
        _services = services;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed Hosted Service is starting..");

        _timer = new Timer(DeleteGameRooms, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        
        return Task.CompletedTask;
    }
    
    private void DeleteGameRooms(object? state)
    {
        using (var scope = _services.CreateScope())
        {
            _logger.LogInformation("Timed Hosted Service [DeleteGameRooms] is working..");

            var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IDataRepository>();
            
            var gameRooms = scopedProcessingService.ListGameRooms();
            
            foreach (var gameRoom in gameRooms)
            {
                var dateTimeWhenToDelete = gameRoom.CreatedOn.AddDays(DaysToKeepGameRoom);
            
                if (DateTime.UtcNow >= dateTimeWhenToDelete)
                {
                    scopedProcessingService.DeleteRoom(gameRoom.ExternalId);
                }
            }
            
            _logger.LogInformation("Timed Hosted Service [DeleteGameRooms] finished working");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping..");

        _timer.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}