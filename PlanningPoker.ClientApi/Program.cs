using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PlanningPoker.Data;
using PlanningPoker.Data.Interfaces;
using PlanningPoker.Services;
using PlanningPoker.Services.Interfaces;
using PlanningPoker.Services.Mapping;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .ReadFrom.Configuration(ctx.Configuration));

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddAutoMapper(typeof(MappingProfile));
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Planning Poker Web API",
            Description = "An ASP.NET Core Web API .NET 6 application that serves as backend part for Planning Poker app."
        });

        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });

    builder.Services.AddDbContext<PlanningPokerDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("PlanningPoker"));
    });
    
    builder.Services.AddHostedService<GameRoomExpirationService>();
    
    builder.Services.AddTransient<IGameRoomService, GameRoomService>();
    builder.Services.AddTransient<IPlayerService, PlayerService>();
    builder.Services.AddTransient<IDataRepository, DataRepository>();
    builder.Services.AddTransient<IPlayerRepository, PlayerRepository>();
    //builder.Services.AddSingleton<IDataRepository, DataRepository>();
    //builder.Services.AddScoped<IDataRepository, DataRepository>();

    var app = builder.Build();
    
    // migrate any database changes on startup (includes initial db creation)
    using (var scope = app.Services.CreateScope())
    {
        var dataContext = scope.ServiceProvider.GetRequiredService<PlanningPokerDbContext>();
        dataContext.Database.Migrate();
    }
    
    app.Services.GetService<IMapper>()!.ConfigurationProvider.AssertConfigurationIsValid();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors(corsPolicyBuilder =>
            {
                corsPolicyBuilder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    //.WithOrigins("http://localhost:5001")
                    .SetIsOriginAllowed(o => true)
                    .AllowCredentials()
                    .Build();
            });
    }
    else
    {
        app.UseHttpsRedirection();
    }
    
    app.UseSerilogRequestLogging();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    // This exception is intentionally suppressed, as it seems to be a design flaw on .NET 6.0
    // and is thrown on running EF Core Migration or Update command
    // https://github.com/dotnet/runtime/issues/60600
    if (!e.GetType().Name.Contains("StopTheHostException"))
    {
        Log.Fatal(e, "Unhandled exception");
    }
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}