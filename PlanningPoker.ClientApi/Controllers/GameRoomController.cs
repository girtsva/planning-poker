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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPost] //HttpPost more appropriate than HttpPut
    //[Route("create")]
    public IActionResult Create([Required]string roomName)
    {
        if (_gameRoomService.RoomNameExists(roomName))
        {
            return Conflict();
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
    /// <param name="id">The id of the room to search for</param>
    /// <response code="200">Success: Returns the found room</response>
    /// <response code="404">Not Found: if room with the specified id does not exist</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("{id}")]
    public IActionResult Get(string id)
    {
        var room = _gameRoomService.GetGameRoomById(id);

        return room == null ? NotFound() : Ok(room);
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
    public IActionResult ListUsers(string roomId)   // to remove
    {
        if (!_gameRoomService.RoomIdExists(roomId))
        {
            return BadRequest($"Room with id {roomId} does not exist!");
        }
        
        return Ok(_gameRoomService.ListUsersInRoom(roomId));
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
    /// <param name="id">The id of the room to delete</param>
    /// <response code="200">Success: Specified room is deleted</response>
    /// <response code="404">Not Found: if room with the specified id does not exist</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(string id)
    {
        if (!_gameRoomService.RoomIdExists(id))
        {
            return NotFound();
        }

        _gameRoomService.DeleteRoom(id);

        return Ok($"Room with id {id} deleted");
    }
    
    [HttpGet]
    [Route("VotingCards")]
    public IActionResult Show()
    {
        var cards = _gameRoomService.ShowVotingCards();

        return Ok(cards);
    }

    [HttpDelete]
    [Route("{roomId}")]
    public IActionResult DeleteAllVotes(string roomId)
    {
        var gameRoom = _gameRoomService.ClearVotes(roomId);

        return Ok(gameRoom);
    }
}