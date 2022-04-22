using Microsoft.Extensions.DependencyInjection;

namespace PlanningPoker.Tests;

/// <summary>
///     Example on how to create ServiceCollection if dependency injection necessary in tests
/// </summary>
public class TestsBase
{
    protected ServiceProvider ServiceCollection;

    public TestsBase()
    {
        var serviceCollection = new ServiceCollection();
        // serviceCollection.AddSingleton(Logging.Instance);
        // serviceCollection.AddScoped<IUserCvService, UserCvService>();
        ServiceCollection = serviceCollection.BuildServiceProvider();

        // return serviceCollection; 
    }
}