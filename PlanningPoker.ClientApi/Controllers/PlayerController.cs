using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Models;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IGameRoomService _gameRoomService;
    private readonly IPlayerService _playerService;

    public PlayerController(IGameRoomService gameRoomService, IPlayerService playerService)
    {
        _gameRoomService = gameRoomService;
        _playerService = playerService;
    }
    
    /// <summary>
    ///     Creates a new player.
    /// </summary>
    /// <param name="playerName">The desired name of the player</param>
    /// <response code="201">Created: confirms the player is created and returns player object</response>
    /// <response code="409">Conflict: if player with the specified name already exists</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPost]
    [Route("create")]
    public IActionResult Create([Required]string playerName)
    {
        if (_playerService.PlayerNameExists(playerName))
        {
            return Conflict();
        }
        
        var player = _playerService.CreatePlayer(playerName);
        
        return Created("", player);
    }
    
    /// <summary>
    /// Gets the list of players.
    /// </summary>
    [HttpGet]
    [Route("list")]
    public IActionResult ListPlayers()
    {
        return Ok(_playerService.ListPlayers());
    }
    
    /// <summary>
    /// Gets the player by its id.
    /// </summary>
    /// <param name="id">The id of the player to search for</param>
    /// <response code="200">Success: Returns the found player</response>
    /// <response code="404">Not Found: if player with the specified id does not exist</response>
    [ProducesResponseType(typeof(Player), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    [Route("{id}")]
    public IActionResult ShowPlayer(string id)
    {
        var player = _playerService.GetPlayerById(id);

        return player == null ? NotFound() : Ok(player);
    }
    
    /// <summary>
    ///     Lets player to join existing game room.
    /// </summary>
    /// <param name="roomId">The id of game room to join</param>
    /// <param name="playerId">The player id who joins the game room</param>
    /// <response code="200">Success: Returns the updated game room object</response>
    /// <response code="400">Bad Request: if player with the specified id does not exist</response>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [HttpPost]
    [Route("join/room/{roomId}/{playerId}")]
    public IActionResult JoinRoom(string roomId, string playerId)
    {
        if (!_gameRoomService.RoomIdExists(roomId))
        {
            return BadRequest($"Room with room Id {roomId} does not exist!");
        }

        if (!_playerService.PlayerIdExists(playerId))
        {
            return BadRequest($"Player with player Id {playerId} does not exist!");
        }

        var player = _playerService.GetPlayerById(playerId);
        var gameRoom = _gameRoomService.AddPlayer(roomId, player!);
        
        return Ok(gameRoom);
    }
}