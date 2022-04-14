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
    /// <response code="201">Created: confirms the room is created and returns room's name</response>
    /// <response code="409">Conflict: if room with the specified name already exists</response>
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
    /// Deletes all rooms.
    /// </summary>
    [HttpDelete]
    [Route("clear")]
    public IActionResult ClearAllRooms()
    {
        _gameRoomService.ClearAllRooms();
        
        return Ok();
    }
}