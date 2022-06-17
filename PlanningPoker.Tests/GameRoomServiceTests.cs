using System;
using System.Collections.Generic;
using System.Threading;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using PlanningPoker.ApiModels.Response;
using PlanningPoker.Common.Models;
using PlanningPoker.Data;
using PlanningPoker.Data.Interfaces;
using PlanningPoker.Models;
using PlanningPoker.Services;
using Serilog;
using Xunit;

namespace PlanningPoker.Tests;

public class GameRoomServiceTests
{
    //private const string ConnectionString = "Server=127.0.0.1; Database=planningPokerDB; User Id=sa; password=Christmas2020; Trusted_Connection=False; MultipleActiveResultSets=true";

    //private readonly Mock<PlanningPokerDbContext> _mockDbContext = new Mock<PlanningPokerDbContext>();
    private readonly GameRoomService _gameRoomService;
    private readonly Mock<IDataRepository> _dataRepositoryMock = new();
    private readonly Mock<ILogger<GameRoomService>> _loggerMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public GameRoomServiceTests()
    {
        _gameRoomService = new GameRoomService(_dataRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void CreateGameRoom_ShouldCreateGameRoom() 
    {
        // Arrange
        var gameRoomExternalId = "jeIEioQkhU";
        var roomName = "testGameRoom";

        var gameRoom = new GameRoom(roomName)
        {
            Id = 1001,
            ExternalId = gameRoomExternalId,
            Name = roomName,
            CreatedOn = It.IsAny<DateTime>()
        };
        
        var gameRoomResponse = new GameRoomResponse
        {
            Id = gameRoomExternalId,
            Name = roomName,
            Players = null,
            Votes = null
        };
        
        _dataRepositoryMock
            .Setup(m => m.CreateGameRoom(roomName))
            .Returns(gameRoom);

        _mapperMock
            .Setup(m => m.Map<GameRoomResponse>(gameRoom))
            .Returns(gameRoomResponse);

        // Act
        var room = _gameRoomService.CreateGameRoom(roomName);
    
        // Assert
        //_dataRepositoryMock.Verify(m => m.CreateGameRoom(roomName), Times.Once);   good for void methods, not appropriate here
        Assert.Equal(gameRoomExternalId, room.Id);
        Assert.Equal(roomName, room.Name);
    }

    [Fact]
    public void ListGameRooms_NoGameRoomsExist_ShouldReturnEmptyList()
    {
        // Arrange
        var gameRoomList = new List<GameRoom>();
        var gameRoomResponseList = new List<GameRoomResponse>();
        
        _dataRepositoryMock
            .Setup(m => m.ListGameRooms())
            .Returns(gameRoomList);

        _mapperMock
            .Setup(m => m.Map<ICollection<GameRoomResponse>>(gameRoomList))
            .Returns(gameRoomResponseList);

        // Act
        var gameRooms = _gameRoomService.ListGameRooms();

        // Assert
        Assert.Empty(gameRooms);
    }
    
    [Fact]
    public void ListGameRooms_GameRoomsExist_ShouldReturnGameRooms()
    {
        // Arrange
        var gameRoomOneName = "TestRoom01";
        var gameRoomTwoName = "TestRoom02";
        var gameRoomThreeName = "TestRoom03";
        var gameRoomOneExternalId = "aBcDeFgHiJ";
        var gameRoomTwoExternalId = "aBcDeFgHiK";
        var gameRoomThreeExternalId = "aBcDeFgHiL";
        var playerOneName = "user01";
        var playerOneExternalId = "4eb1f3fb3f";
        var playerTwoName = "user02";
        var playerTwoExternalId = "5ac3g4yu0t";

        var gameRoomOne = new GameRoom(gameRoomOneName)
        {
            Id = 1001,
            ExternalId = gameRoomOneExternalId,
            Name = gameRoomOneName,
            CreatedOn = It.IsAny<DateTime>(),
            Players = new List<Player>
            {
                new Player(playerOneName) { Id = 2001, ExternalId = playerOneExternalId, Name = playerOneName },
                new Player(playerTwoName) { Id = 2002, ExternalId = playerTwoExternalId, Name = playerTwoName }
            },
            Votes = new List<PlayerVote>
            {
                new PlayerVote(playerOneExternalId, VotingCard.Eight),
                new PlayerVote(playerTwoExternalId, VotingCard.Thirteen)
            }
        };
        
        var gameRoomTwo = new GameRoom(gameRoomTwoName)
        {
            Id = 1002,
            ExternalId = gameRoomTwoExternalId,
            Name = gameRoomTwoName,
            CreatedOn = It.IsAny<DateTime>(),
            Players = new List<Player>
            {
                new Player(playerOneName) { Id = 2001, ExternalId = playerOneExternalId, Name = playerOneName },
                new Player(playerTwoName) { Id = 2002, ExternalId = playerTwoExternalId, Name = playerTwoName }
            }
        };
        
        var gameRoomThree = new GameRoom(gameRoomThreeName)
        {
            Id = 1003,
            ExternalId = gameRoomThreeExternalId,
            Name = gameRoomThreeName,
            CreatedOn = It.IsAny<DateTime>()
        };
        
        var gameRoomList = new List<GameRoom>
        {
            gameRoomOne,
            gameRoomTwo,
            gameRoomThree
        };
        
        var gameRoomOneResponse = new GameRoomResponse
        {
            Id = gameRoomOneExternalId,
            Name = gameRoomOneName,
            Players = new List<PlayerResponse>
            {
                new PlayerResponse { Id = playerOneExternalId, Name = playerOneName},
                new PlayerResponse { Id = playerTwoExternalId, Name = playerTwoName}
            },
            Votes = new List<PlayerVoteResponse>
            {
                new PlayerVoteResponse { PlayerId = playerOneExternalId, Value = VotingCard.Eight },
                new PlayerVoteResponse { PlayerId = playerTwoExternalId, Value = VotingCard.Thirteen }
            }
        };
        
        var gameRoomTwoResponse = new GameRoomResponse
        {
            Id = gameRoomTwoExternalId,
            Name = gameRoomTwoName,
            Players = new List<PlayerResponse>
            {
                new PlayerResponse { Id = playerOneExternalId, Name = playerOneName},
                new PlayerResponse { Id = playerTwoExternalId, Name = playerTwoName}
            }
        };
        
        var gameRoomThreeResponse = new GameRoomResponse
        {
            Id = gameRoomThreeExternalId,
            Name = gameRoomThreeName
        };
        
        var gameRoomResponseList = new List<GameRoomResponse>
        {
            gameRoomOneResponse,
            gameRoomTwoResponse,
            gameRoomThreeResponse
        };
        
        _dataRepositoryMock
            .Setup(m => m.ListGameRooms())
            .Returns(gameRoomList);

        _mapperMock
            .Setup(m => m.Map<ICollection<GameRoomResponse>>(gameRoomList))
            .Returns(gameRoomResponseList);

        // Act
        var gameRooms = _gameRoomService.ListGameRooms();

        // Assert
        Assert.Equal(JsonConvert.SerializeObject(gameRoomResponseList), JsonConvert.SerializeObject(gameRooms));
    }
    
    [Fact]
    public void GetGameRoomById_GameRoomExists_ShouldReturnSpecifiedGameRoomById() 
    {
        // Arrange
        var gameRoomExternalId = "jeIEioQkhU";
        var roomName = "testGameRoom";
        var playerOneName = "user01";
        var playerOneExternalId = "4eb1f3fb3f";
        var playerTwoName = "user02";
        var playerTwoExternalId = "5ac3g4yu0t";

        var gameRoom = new GameRoom(roomName)
        {
            Id = 1001,
            ExternalId = gameRoomExternalId,
            Name = roomName,
            CreatedOn = It.IsAny<DateTime>(),
            Players = new List<Player>()
            {
                new Player(playerOneName) { Id = 2001, ExternalId = playerOneExternalId, Name = playerOneName},
                new Player(playerTwoName) { Id = 2002, ExternalId = playerTwoExternalId, Name = playerTwoName}
            },
            Votes = null
        };
        
        var gameRoomResponse = new GameRoomResponse
        {
            Id = gameRoomExternalId,
            Name = roomName,
            Players = new List<PlayerResponse>()
            {
                new PlayerResponse { Id = playerOneExternalId, Name = playerOneName},
                new PlayerResponse { Id = playerTwoExternalId, Name = playerTwoName}
            },
            Votes = null
        };
        
        _dataRepositoryMock
            .Setup(m => m.GetGameRoomById(gameRoomExternalId))
            .Returns(gameRoom);

        _mapperMock
            .Setup(m => m.Map<GameRoomResponse>(gameRoom))
            .Returns(gameRoomResponse);

        // Act
        var room = _gameRoomService.GetGameRoomById(gameRoomExternalId);
    
        // Assert
        _dataRepositoryMock.Verify(m => m.GetGameRoomById(gameRoomExternalId), Times.Once);
        Assert.Equal(JsonConvert.SerializeObject(gameRoomResponse), JsonConvert.SerializeObject(room));
    }
    
    [Fact]
    public void GetGameRoomById_NoGameRoomExists_ShouldReturnNull() 
    {
        // Arrange
        _dataRepositoryMock
            .Setup(m => m.GetGameRoomById(It.IsAny<string>()))
            .Returns(() => null);

        _mapperMock
            .Setup(m => m.Map<GameRoomResponse>(null))
            .Returns(() => null);

        // Act
        var room = _gameRoomService.GetGameRoomById(It.IsAny<string>());
    
        // Assert
        Assert.Null(room);
    }

    [Fact]
    public void AddPlayer_ShouldReturnGameRoomWithAddedPlayer()
    {
        // Arrange
        var gameRoomExternalId = "jeIEioQkhU";
        var roomName = "testGameRoom";
        var playerName = "user01";
        var playerExternalId = "4eb1f3fb3f";
        
        var gameRoom = new GameRoom(roomName)
        {
            Id = 1001,
            ExternalId = gameRoomExternalId,
            Name = roomName,
            CreatedOn = It.IsAny<DateTime>()
        };
        
        var gameRoomResponse = new GameRoomResponse
        {
            Id = gameRoomExternalId,
            Name = roomName,
            Players = new List<PlayerResponse>()
            {
                new PlayerResponse { Id = playerExternalId, Name = playerName}
            }
        };
        
        _dataRepositoryMock
            .Setup(m => m.AddPlayer(gameRoomExternalId, playerName))
            .Returns(gameRoom);

        _mapperMock
            .Setup(m => m.Map<GameRoomResponse>(gameRoom))
            .Returns(gameRoomResponse);

        // Act
        var room = _gameRoomService.AddPlayer(gameRoomExternalId, playerName);

        // Assert
        Assert.Equal(JsonConvert.SerializeObject(gameRoomResponse), JsonConvert.SerializeObject(room));
    }

    [Fact]
    public void ListPlayersInRoom_NoPlayersExist_ShouldReturnEmptyList()
    {
        // Arrange
        var gameRoomName = "TestRoom01";
        var gameRoomExternalId = "aBcDeFgHiJ";

        var gameRoom = new GameRoom(gameRoomName)
        {
            Id = 1002,
            ExternalId = gameRoomExternalId,
            Name = gameRoomName,
            CreatedOn = It.IsAny<DateTime>()
        };

        var playerResponseList = new List<PlayerResponse>();

        _dataRepositoryMock
            .Setup(m => m.ListPlayersInRoom(gameRoomExternalId))
            .Returns(gameRoom.Players);

        _mapperMock
            .Setup(m => m.Map<ICollection<PlayerResponse>>(gameRoom.Players))
            .Returns(playerResponseList);

        // Act
        var players = _gameRoomService.ListPlayersInRoom(gameRoomExternalId);

        // Assert
        Assert.Equal(playerResponseList, players);
        Assert.Empty(players);
    }
    
    [Fact]
    public void ListPlayersInRoom_PlayersExist_ShouldReturnListOfPlayers()
    {
        // Arrange
        var gameRoomName = "TestRoom01";
        var gameRoomExternalId = "aBcDeFgHiJ";
        var playerOneName = "user01";
        var playerTwoName = "user02";
        var playerOneExternalId = "4eb1f3fb3f";
        var playerTwoExternalId = "5ac3g4yu0t";
        
        var gameRoom = new GameRoom(gameRoomName)
        {
            Id = 1002,
            ExternalId = gameRoomExternalId,
            Name = gameRoomName,
            CreatedOn = It.IsAny<DateTime>(),
            Players = new List<Player>
            {
                new Player(playerOneName) { Id = 2001, ExternalId = playerOneExternalId, Name = playerOneName },
                new Player(playerTwoName) { Id = 2002, ExternalId = playerTwoExternalId, Name = playerTwoName }
            }
        };

        var playerResponseList = new List<PlayerResponse>
        {
            new PlayerResponse { Id = playerOneExternalId, Name = playerOneName},
            new PlayerResponse { Id = playerTwoExternalId, Name = playerTwoName}
        };

        _dataRepositoryMock
            .Setup(m => m.ListPlayersInRoom(gameRoomExternalId))
            .Returns(gameRoom.Players);

        _mapperMock
            .Setup(m => m.Map<ICollection<PlayerResponse>>(gameRoom.Players))
            .Returns(playerResponseList);

        // Act
        var players = _gameRoomService.ListPlayersInRoom(gameRoomExternalId);

        // Assert
        Assert.Equal(playerResponseList, players);
    }
    
    [Fact]
    public void RemovePlayer_ShouldReturnGameRoomWithoutRemovedPlayer()
    {
        // Arrange
        var gameRoomExternalId = "jeIEioQkhU";
        var roomName = "testGameRoom";
        var playerOneName = "user01";
        var playerOneExternalId = "4eb1f3fb3f";
        var playerTwoName = "user02";
        var playerTwoExternalId = "5ac3g4yu0t";
        
        var gameRoom = new GameRoom(roomName)
        {
            Id = 1001,
            ExternalId = gameRoomExternalId,
            Name = roomName,
            CreatedOn = It.IsAny<DateTime>(),
            Players = new List<Player>
            {
                new Player(playerOneName) { Id = 2001, ExternalId = playerOneExternalId, Name = playerOneName },
                new Player(playerTwoName) { Id = 2002, ExternalId = playerTwoExternalId, Name = playerTwoName }
            },
        };
        
        var gameRoomResponse = new GameRoomResponse
        {
            Id = gameRoomExternalId,
            Name = roomName,
            Players = new List<PlayerResponse>()
            {
                new PlayerResponse { Id = playerTwoExternalId, Name = playerTwoName}
            }
        };
        
        _dataRepositoryMock
            .Setup(m => m.RemovePlayer(gameRoomExternalId, playerOneExternalId))
            .Returns(gameRoom);

        _mapperMock
            .Setup(m => m.Map<GameRoomResponse>(gameRoom))
            .Returns(gameRoomResponse);

        // Act
        var room = _gameRoomService.RemovePlayer(gameRoomExternalId, playerOneExternalId);

        // Assert
        Assert.Equal(JsonConvert.SerializeObject(gameRoomResponse), JsonConvert.SerializeObject(room));
    }
    
    
    
    [Fact(Skip = "This test is broken - DbContext options???")]
    public void CreateGameRoom_SavesGameRoomViaContext() 
    {
        // Arrange
        string ConnectionString =
            "Server=127.0.0.1; Database=planningPokerDB; User Id=sa; password=Christmas2020; Trusted_Connection=False; MultipleActiveResultSets=true";
        
        var mockDbContext = new Mock<PlanningPokerDbContext>
            (ConnectionString);
        var roomMock = new Mock<DbSet<GameRoom>>();
        
        var testDataRepo = new DataRepository(mockDbContext.Object);
        var roomName = "testGameRoom";
        
        mockDbContext.Setup(m => m.Set<GameRoom>()).Returns(roomMock.Object);
    
        // Act
        testDataRepo.CreateGameRoom(roomName);
    
        // Assert
        roomMock.Verify(m => m.Add(It.IsAny<GameRoom>()), Times.Once());
        mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    // [Fact]
    // public void CreateGameRoom_NoGameRoomsExist_ReturnsCreatedGameRoom() 
    // {
    //     // Arrange
    //     var mockDbContext = new Mock<PlanningPokerDbContext>();
    //     var roomMock = new Mock<DbSet<GameRoom>>();
    //     
    //     var testDataRepo = new DataRepository(mockDbContext.Object);
    //     var roomName = "testGameRoom";
    //     var gameRoom = new GameRoom(roomName);
    //
    //     roomMock.Setup(room => room.Add(It.IsAny<GameRoom>())).Returns(GameRoom room);
    //     mockDbContext.Setup(m => m.GameRooms).Returns(roomMock.Object);
    //
    //     // Act
    //     var gameRoomCreated = testDataRepo.CreateGameRoom(roomName);
    //
    //     // Assert
    //     Assert.Equal(JsonConvert.SerializeObject(gameRoomCreated), JsonConvert.SerializeObject(gameRoomFound));
    // }
    //
    // [Fact]
    // public void GetGameRoomById_GameRoomsExist_ReturnsGameRoom() 
    // {
    //     // Arrange
    //     var testDataRepo = new DataRepository(_mockDbContext.Object);
    //     var roomName = "testGameRoom";
    //     var gameRoomCreated = testDataRepo.CreateGameRoom(roomName);
    //
    //     // Act
    //     var gameRoomFound = testDataRepo.GetGameRoomById(gameRoomCreated.ExternalId);
    //
    //     // Assert
    //     Assert.Equal(JsonConvert.SerializeObject(gameRoomCreated), JsonConvert.SerializeObject(gameRoomFound));
    // }
    // [Fact]
    // public void GetGameRoomById_NoGameRoomsExist_ReturnsNull()
    // {
    //     // Arrange
    //     var testDataRepo = new DataRepository(_mockDbContext.Object);
    //     var roomId = "testRoomId";
    //
    //     // Act
    //     var gameRoom = testDataRepo.GetGameRoomById(roomId);
    //     
    //     // Assert
    //     Assert.True(gameRoom == null);
    // }
}