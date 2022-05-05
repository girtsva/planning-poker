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
    
    // /// <summary>
    // ///     Creates a new player.
    // /// </summary>
    // /// <param name="playerName">The desired name of the player</param>
    // /// <response code="201">Created: confirms the player is created and returns player object</response>
    // /// <response code="409">Conflict: if player with the specified name already exists</response>
    // [ProducesResponseType(StatusCodes.Status201Created)]
    // [ProducesResponseType(StatusCodes.Status409Conflict)]
    // [HttpPost]
    // [Route("create")]
    // public IActionResult Create([Required]string playerName)
    // {
    //     if (_playerService.PlayerNameExists(playerName))
    //     {
    //         return Conflict();
    //     }
    //     
    //     var player = _playerService.CreatePlayer(playerName);
    //     
    //     return Created("", player);
    // }
    //
    // /// <summary>
    // /// Gets the list of players.
    // /// </summary>
    // [HttpGet]
    // [Route("list")]
    // public IActionResult ListPlayers()
    // {
    //     return Ok(_playerService.ListPlayers());
    // }
    //
    // /// <summary>
    // /// Gets the player by its id.
    // /// </summary>
    // /// <param name="id">The id of the player to search for</param>
    // /// <response code="200">Success: Returns the found player</response>
    // /// <response code="404">Not Found: if player with the specified id does not exist</response>
    // [ProducesResponseType(typeof(Player), 200)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [HttpGet]
    // [Route("{id}")]
    // public IActionResult ShowPlayer(string id)
    // {
    //     var player = _playerService.GetPlayerById(id);
    //
    //     return player == null ? NotFound() : Ok(player);
    // }
    
    /// <summary>
    ///     Lets player to join existing game room.
    /// </summary>
    /// <param name="roomId">The id of game room to join</param>
    /// <param name="playerName">The player name who joins the game room</param>
    /// <response code="200">Success: Returns the updated game room object</response>
    /// <response code="400">Bad Request: if player with the specified name does not exist</response>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [HttpPost]
    [Route("Join/Room/{roomId}/{playerName}")]
    public IActionResult JoinRoom(
        [RegularExpression("[a-zA-Z]{10}", ErrorMessage = "Incorrect room id")]
        string roomId,
        [RegularExpression("[a-zA-Z0-9_-]{3,20}", ErrorMessage = "Player name must be 3-20 characters; only alphanumeric, _,- characters allowed")]
        string playerName)
    {
        if (!_gameRoomService.RoomIdExists(roomId))
        {
            return BadRequest($"Room with id {roomId} does not exist!");
        }

        // if (!_playerService.PlayerNameExists(playerName))
        // {
        //     return BadRequest($"Player with name {playerName} does not exist!");
        // }
        
        if (_playerService.PlayerNameExists(roomId, playerName))
        {
            return BadRequest($"Player with name {playerName} already exists in the room with id {roomId}!");
        }

        // var player = _playerService.GetPlayerByName(playerName);
        // var gameRoom = _gameRoomService.AddPlayer(roomId, player!);
        
        var gameRoom = _gameRoomService.AddPlayer(roomId, playerName);
        
        return Ok(gameRoom);
    }
    
    /// <summary>
    ///     Removes player from existing game room.
    /// </summary>
    /// <param name="roomId">The id of game room from which the player will be removed</param>
    /// <param name="playerId">The id of the player to be removed</param>
    /// <response code="200">Success: Returns the updated game room object</response>
    /// <response code="400">Bad Request: if player with the specified name does not exist</response>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [HttpDelete]
    [Route("Leave/Room/{roomId}/{playerId}")]
    public IActionResult LeaveRoom(
        [RegularExpression("[a-zA-Z]{10}", ErrorMessage = "Incorrect room id")]
        string roomId,
        [RegularExpression("[a-zA-Z0-9]{10}", ErrorMessage = "Incorrect player id")]
        string playerId)
    {
        if (!_gameRoomService.RoomIdExists(roomId))
        {
            return BadRequest($"Room with id {roomId} does not exist!");
        }

        if (!_playerService.PlayerIdExists(roomId, playerId))
        {
            return BadRequest($"Player with id {playerId} does not exist in the room with id {roomId}!");
        }

        var gameRoom = _gameRoomService.RemovePlayer(roomId, playerId);
        
        return Ok(gameRoom);
    }

    /// <summary>
    ///     Submits the vote of the specified player in the specified game room.
    /// </summary>
    /// <param name="roomId">The id of game room in which the voting process is happening</param>
    /// <param name="playerId">The id of player who has voted</param>
    /// <param name="vote">The vote value chosen</param>
    /// <returns>Updated instance of game room.</returns>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [HttpPut]
    [Route("Vote/{roomId}/{playerId}")]
    public IActionResult Vote(
        [RegularExpression("[a-zA-Z]{10}", ErrorMessage = "Incorrect room id")]
        string roomId, 
        [RegularExpression("[a-zA-Z0-9]{10}", ErrorMessage = "Incorrect player id")]
        string playerId, 
        [Required]
        PlayerVote vote)
    {
        if (!_gameRoomService.RoomIdExists(roomId))
        {
            return BadRequest($"Room with id {roomId} does not exist!");
        }

        if (!_playerService.PlayerIdExists(roomId, playerId))
        {
            return BadRequest($"Player with id {playerId} does not exist in the room with id {roomId}!");
        }

        // if (!_gameRoomService.VoteExists(vote))
        // {
        //     return BadRequest($"Provided vote value {vote} does not exist!");
        // }
        
        var gameRoom = _playerService.Vote(roomId, playerId, vote);

        return Ok(gameRoom);
    }
}