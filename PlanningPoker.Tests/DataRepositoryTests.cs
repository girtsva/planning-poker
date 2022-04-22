using PlanningPoker.Data;
using Xunit;

namespace PlanningPoker.Tests;

public class DataRepositoryTests
{
    [Fact]
    public void GetGameRoomByName_GameRoomsExist_ReturnsGameRoom()
    {
        // Arrange
        var testDataRepo = new DataRepository();
        var roomName = "testRepo";
        testDataRepo.CreateGameRoom(roomName);

        // Act
        var gameRoom = testDataRepo.GetGameRoomByName(roomName);

        // Assert
        Assert.True(gameRoom!.Name == roomName);
    }
    [Fact]
    public void GetGameRoomByName_NoGameRoomsExist_ReturnsNull()
    {
        // Arrange
        var testDataRepo = new DataRepository();
        var roomName = "testRepo2";

        // Act
        var gameRoom = testDataRepo.GetGameRoomByName(roomName);
        
        // Assert
        Assert.True(gameRoom == null);
    }
}