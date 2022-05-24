using JetBrains.Annotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using PlanningPoker.Data;
using PlanningPoker.Data.Interfaces;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Services.HealthChecks;

[UsedImplicitly]
public class PpHealthCheck : IHealthCheck
{
    private readonly IGameRoomService _gameRoomService;
    private readonly IPlayerService _playerService;
    private readonly IDataRepository _dataRepository;
    private readonly PlanningPokerDbContext _dbContext;
    private readonly ILogger<PpHealthCheck> _logger;

    public PpHealthCheck(
        IGameRoomService gameRoomService,
        IPlayerService playerService,
        IDataRepository dataRepository,
        PlanningPokerDbContext dbContext,
        ILogger<PpHealthCheck> logger)
    {
        _gameRoomService = gameRoomService;
        _playerService = playerService;
        _dataRepository = dataRepository;
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}