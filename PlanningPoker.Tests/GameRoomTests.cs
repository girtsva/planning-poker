using System.Collections.Generic;
using System.Linq;
using PlanningPoker.Models;
using PlanningPoker.Services.Interfaces;
using Xunit;

namespace PlanningPoker.Tests;

public class GameRoomTests : TestsBase
{
    [Fact]
    public void GenerateRandomId_LenghtAndContents_ReturnsLenght10AndOnlyLetters()
    {
        ServiceCollection.GetService(typeof(IGameRoomService));
        // Arrange
        var gameRoom = new GameRoom("testRoom");

        // Act
        var id = gameRoom.Id;

        // Assert
        Assert.True(id.Length == 10);
        Assert.True(id.All(char.IsLetter));
    }

    // [Fact]
    // public void GenerateRandomId_UppercaseLowercase_IdContainsBothUppercaseLowercaseLetters()
    // {
    //     // Arrange
    //     
    //     
    //     // Act
    //     var id = GameRoom.GenerateRandomId();
    //
    //     // Assert
    //     Assert.Contains(id, char.IsUpper);
    //     Assert.Contains(id, char.IsLower);
    // }
    [Fact]
    public void GenerateRandomId_Inequality_ReturnsDifferentIds()
    {
        // Arrange
        var ids = new HashSet<string>();
        var numberOfRooms = 30;

        // Act
        for (var i = 0; i < numberOfRooms; i++)
        {
            var gameRoom = new GameRoom("testRoom");
            ids.Add(gameRoom.Id);
        }

        // Assert
        Assert.True(ids.Count == numberOfRooms);
        //CollectionAssert.AllItemsAreUnique  analogue for xunit?
    }
}