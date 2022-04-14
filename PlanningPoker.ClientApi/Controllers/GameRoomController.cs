using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Data;
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
    [HttpPut]
    [Route("create")]
    public IActionResult CreateGameRoom(string roomName)
    {
        if (DataStorage.Exists(roomName))
        {
            return Conflict();
        }
        
        _gameRoomService.CreateGameRoom(new GameRoom(roomName));
        
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
}