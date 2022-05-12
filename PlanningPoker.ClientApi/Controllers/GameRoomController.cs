using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Models;
using PlanningPoker.Services.Interfaces;
using PlanningPoker.Validation;

namespace PlanningPoker.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class GameRoomController : ControllerBase
{
    private readonly IGameRoomService _gameRoomService;

    public GameRoomController(IGameRoomService gameRoomService)
    {
        _gameRoomService = gameRoomService;
    }
    
    /// <summary>
    ///     Creates a new game room.
    /// </summary>
    /// <param name="roomName">The desired name of the room to create</param>
    /// <response code="201">Created: confirms the room is created and returns game room object</response>
    /// <response code="400">Bad Request: if incorrectly entered roomName or if room with the specified name already exists</response>
    /// <returns>Instance of created game room.</returns>
    [ProducesResponseType(typeof(GameRoom), 201)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public IActionResult Create(
        [Required]
        [RoomNameValidation]
        string roomName)
    {
        return Created("", _gameRoomService.CreateGameRoom(roomName));
    }
    
    /// <summary>
    ///     Gets the list of game rooms.
    /// </summary>
    [HttpGet]
    public IActionResult List()
    {
        return Ok(_gameRoomService.ListGameRooms());
    }
    
    /// <summary>
    ///     Gets the game room by its id.
    /// </summary>
    /// <param name="roomId">The id of the room to search for</param>
    /// <response code="200">Success: Returns the found room</response>
    /// <response code="400">Bad Request: if room id is not valid or the room with the specified id does not exist</response>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    [Route("{roomId}")]
    public IActionResult Get(
        [RoomIdValidation]
        string roomId)
    {
        return Ok(_gameRoomService.GetGameRoomById(roomId));
    }
    
    /// <summary>
    ///     Gets the list of players in the specified room.
    /// </summary>
    /// <param name="roomId">The id of game room for which to show the players</param>
    /// <response code="200">Success: Returns the list of players</response>
    /// <response code="400">Bad Request: if room id is not valid or the room with the specified id does not exist</response>
    [ProducesResponseType(typeof(List<Player>), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    [Route("{roomId}")]
    public IActionResult ListPlayers(
        [RoomIdValidation]
        string roomId)
    {
        return Ok(_gameRoomService.ListPlayersInRoom(roomId));
    }
    
    /// <summary>
    ///     Removes all players from the specified room.
    /// </summary>
    /// <param name="roomId">The id of game room from which the players will be removed</param>
    /// <response code="200">Success: All players are deleted</response>
    /// <response code="400">Bad Request: if room id is not valid or the room with the specified id does not exist</response>
    /// <returns>Instance of updated game room.</returns>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete]
    [Route("{roomId}")]
    public IActionResult RemoveAllPlayers(
        [RoomIdValidation]
        string roomId)
    {
        return Ok(_gameRoomService.RemoveAllPlayers(roomId));
    }

    /// <summary>
    ///     Deletes all rooms.
    /// </summary>
    [HttpDelete]
    public IActionResult DeleteAll()
    {
        _gameRoomService.DeleteAllRooms();
        
        return Ok();
    }
    
    /// <summary>
    ///     Deletes the game room by its id.
    /// </summary>
    /// <param name="roomId">The id of the room to delete</param>
    /// <response code="200">Success: Specified room is deleted</response>
    /// <response code="400">Bad Request: if room id is not valid or the room with the specified id does not exist</response>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete]
    [Route("{roomId}")]
    public IActionResult Delete(
        [RoomIdValidation]
        string roomId)
    {
        _gameRoomService.DeleteRoom(roomId);

        return Ok($"Room with id {roomId} deleted");
    }
    
    /// <summary>
    ///     Shows voting card values (e.g., Fibonacci number sequence).
    /// </summary>
    /// <returns>Array of voting card values.</returns>
    [ProducesResponseType(typeof(ICollection<int>), 200)]
    [HttpGet]
    [Route("VotingCards")]
    public IActionResult Show()
    {
        return Ok(_gameRoomService.ShowVotingCards());
    }

    /// <summary>
    ///     Deletes all votes in the given game room.
    /// </summary>
    /// <param name="roomId">The id of game room from which the votes will be removed</param>
    /// <response code="200">Success: Votes in the specified room are deleted</response>
    /// <response code="400">Bad Request: if room id is not valid or the room with the specified id does not exist</response>
    /// <returns>Instance of updated game room.</returns>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete]
    [Route("{roomId}")]
    public IActionResult DeleteAllVotes(
        [RoomIdValidation]
        string roomId)
    {
        return Ok(_gameRoomService.ClearVotes(roomId));
    }
}