// using Newtonsoft.Json;
// using PlanningPoker.Data;
// using Xunit;
//
// namespace PlanningPoker.Tests;
//
// public class DataRepositoryTests
// {
//     [Fact]
//     public void GetGameRoomById_GameRoomsExist_ReturnsGameRoom()
//     {
//         // Arrange
//         var testDataRepo = new DataRepository();
//         var roomName = "testGameRoom";
//         var gameRoomCreated = testDataRepo.CreateGameRoom(roomName);
//
//         // Act
//         var gameRoomFound = testDataRepo.GetGameRoomById(gameRoomCreated.ExternalId);
//
//         // Assert
//         Assert.Equal(JsonConvert.SerializeObject(gameRoomCreated), JsonConvert.SerializeObject(gameRoomFound));
//     }
//     [Fact]
//     public void GetGameRoomById_NoGameRoomsExist_ReturnsNull()
//     {
//         // Arrange
//         var testDataRepo = new DataRepository();
//         var roomId = "testRoomId";
//
//         // Act
//         var gameRoom = testDataRepo.GetGameRoomById(roomId);
//         
//         // Assert
//         Assert.True(gameRoom == null);
//     }
// }