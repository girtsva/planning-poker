using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Models;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Controllers;

[Route("api/[controller]")]
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
    /// <response code="201">Created: confirms the room is created and returns room's name</response>
    /// <response code="409">Conflict: if room with the specified name already exists</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPut]
    [Route("create")]
    public IActionResult CreateGameRoom(string roomName)
    {
        if (_gameRoomService.RoomNameExists(roomName))
        {
            return Conflict();
        }
        
        _gameRoomService.CreateGameRoom(roomName, new GameRoom(roomName));
        
        return Created("", roomName);
    }
    
    /// <summary>
    /// Gets the list of game rooms.
    /// </summary>
    [HttpGet]
    [Route("list")]
    public IActionResult ListRooms()
    {
        return Ok(_gameRoomService.ListGameRooms());
    }
    
    /// <summary>
    /// Gets the game room by its name.
    /// </summary>
    /// <param name="name">The name of the room to search for</param>
    /// <response code="200">Success: Returns the found room</response>
    /// <response code="404">Not Found: if room with the specified name does not exist</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("list/{name}")]
    public IActionResult ShowRoomByName(string name)
    {
        var room = _gameRoomService.GetGameRoomByName(name);
        
        if (room == null)
        {
            return NotFound();
        }

        return Ok(room);
    }
    
    /// <summary>
    /// Deletes all rooms.
    /// </summary>
    [HttpDelete]
    [Route("clear")]
    public IActionResult ClearAllRooms()
    {
        _gameRoomService.ClearAllRooms();
        
        return Ok();
    }
    
    /// <summary>
    /// Deletes the game room by its name.
    /// </summary>
    /// <param name="name">The name of the room to delete</param>
    /// <response code="200">Success: Specified room is deleted</response>
    /// <response code="404">Not Found: if room with the specified name does not exist</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("clear/{name}")]
    public IActionResult DeleteRoomByName(string name)
    {
        if (!_gameRoomService.RoomNameExists(name))
        {
            return NotFound();
        }

        _gameRoomService.DeleteRoom(name);

        return Ok($"Room {name} deleted");
    }
}