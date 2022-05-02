using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Models;
using PlanningPoker.Services.Interfaces;

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
    /// Creates a new game room.
    /// </summary>
    /// <param name="roomName">The desired name of the room to create</param>
    /// <response code="201">Created: confirms the room is created and returns game room object</response>
    /// <response code="409">Conflict: if room with the specified name already exists</response>
    [ProducesResponseType(typeof(GameRoom), 201)]
    [ProducesResponseType(typeof(string), 409)]
    [HttpPost] //HttpPost more appropriate than HttpPut
    //[Route("create")]
    public IActionResult Create([Required]string roomName)
    {
        if (_gameRoomService.RoomNameExists(roomName))
        {
            return Conflict($"Room with name {roomName} already exists!");
        }
        
        var gameRoom = _gameRoomService.CreateGameRoom(roomName);
        
        return Created("", gameRoom);
    }
    
    /// <summary>
    /// Gets the list of game rooms.
    /// </summary>
    [HttpGet]
    //[Route("list")]
    public IActionResult List()
    {
        return Ok(_gameRoomService.ListGameRooms());
    }
    
    /// <summary>
    /// Gets the game room by its id.
    /// </summary>
    /// <param name="roomId">The id of the room to search for</param>
    /// <response code="200">Success: Returns the found room</response>
    /// <response code="404">Not Found: if room with the specified id does not exist</response>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(typeof(string), 404)]
    [HttpGet]
    [Route("{roomId}")]
    public IActionResult Get(string roomId)
    {
        var room = _gameRoomService.GetGameRoomById(roomId);

        return room == null ? NotFound($"No room found with id {roomId} ") : Ok(room);
    }
    
    /// <summary>
    /// Gets the list of players in the specified room.
    /// </summary>
    /// <response code="200">Success: Returns the list of players</response>
    /// <response code="400">Bad Request: if room with the specified id does not exist</response>
    [ProducesResponseType(typeof(List<Player>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [HttpGet]
    [Route("{roomId}")]
    public IActionResult ListPlayers(string roomId)   // to remove
    {
        if (!_gameRoomService.RoomIdExists(roomId))
        {
            return BadRequest($"Room with id {roomId} does not exist!");
        }
        
        return Ok(_gameRoomService.ListPlayersInRoom(roomId));
    }
    
    /// <summary>
    ///     Removes all players from the specified room.
    /// </summary>
    /// <param name="roomId">The id of game room from which the players will be removed</param>
    /// <response code="200">Success: All players are deleted</response>
    /// <returns>Instance of updated game room.</returns>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [HttpDelete]
    [Route("{roomId}")]
    public IActionResult RemoveAllPlayers(string roomId)
    {
        if (!_gameRoomService.RoomIdExists(roomId))
        {
            return BadRequest($"Room with id {roomId} does not exist!");
        }
        
        var gameRoom = _gameRoomService.RemoveAllPlayers(roomId);

        return Ok(gameRoom);
    }

    /// <summary>
    /// Deletes all rooms.
    /// </summary>
    [HttpDelete]
    //[Route("delete")]
    public IActionResult DeleteAll()
    {
        _gameRoomService.DeleteAllRooms();
        
        return Ok();
    }
    
    /// <summary>
    /// Deletes the game room by its id.
    /// </summary>
    /// <param name="roomId">The id of the room to delete</param>
    /// <response code="200">Success: Specified room is deleted</response>
    /// <response code="404">Not Found: if room with the specified id does not exist</response>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(typeof(string), 404)]
    [HttpDelete]
    [Route("{roomId}")]
    public IActionResult Delete(string roomId)
    {
        if (!_gameRoomService.RoomIdExists(roomId))
        {
            return NotFound($"Room with id {roomId} does not exist!");
        }

        _gameRoomService.DeleteRoom(roomId);

        return Ok($"Room with id {roomId} deleted");
    }
    
    /// <summary>
    ///     Shows voting card values (e.g., Fibonacci number sequence).
    /// </summary>
    /// <returns>Array of voting card values.</returns>
    [ProducesResponseType(typeof(Array), 200)]
    [HttpGet]
    [Route("VotingCards")]
    public IActionResult Show()
    {
        var cards = _gameRoomService.ShowVotingCards();

        return Ok(cards);
    }

    /// <summary>
    ///     Deletes all votes in the given game room.
    /// </summary>
    /// <param name="roomId">The id of game room from which the votes will be removed</param>
    /// <returns>Instance of updated game room.</returns>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [HttpDelete]
    [Route("{roomId}")]
    public IActionResult DeleteAllVotes(string roomId)
    {
        if (!_gameRoomService.RoomIdExists(roomId))
        {
            return BadRequest($"Room with id {roomId} does not exist!");
        }
        
        var gameRoom = _gameRoomService.ClearVotes(roomId);

        return Ok(gameRoom);
    }
}