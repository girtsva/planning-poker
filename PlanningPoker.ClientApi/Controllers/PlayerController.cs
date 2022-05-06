using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Models;
using PlanningPoker.Services.Interfaces;
using PlanningPoker.Validation;

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
    ///     Lets player to join existing game room.
    /// </summary>
    /// <param name="roomId">The id of game room to join</param>
    /// <param name="playerName">The player name who joins the game room</param>
    /// <response code="200">Success: Returns the updated game room object</response>
    /// <response code="400">Bad Request:
    /// if incorrectly entered roomId / playerName or
    /// if room with the specified id does not exist</response>
    /// <response code="409">Conflict: if player with the specified name already exists in the specified room</response>
    /// <returns>Updated instance of game room.</returns>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), 409)]
    [HttpPost]
    [Route("Join/Room/{roomId}/{playerName}")]
    public IActionResult JoinRoom(
        [RoomIdValidation]
        string roomId,
        [PlayerNameValidation]
        string playerName)
    {
        if (_playerService.PlayerNameExists(roomId, playerName))
        {
            return Conflict($"Player with name {playerName} already exists in the room with id {roomId}!");
        }

        return Ok(_gameRoomService.AddPlayer(roomId, playerName));
    }
    
    /// <summary>
    ///     Removes player from existing game room.
    /// </summary>
    /// <param name="roomId">The id of game room from which the player will be removed</param>
    /// <param name="playerId">The id of the player to be removed</param>
    /// <response code="200">Success: Returns the updated game room object</response>
    /// <response code="400">Bad Request:
    /// if incorrectly entered roomId / playerId or
    /// if room with the specified id does not exist or
    /// if player with the specified id does not exist in the specified room</response>
    /// <returns>Updated instance of game room.</returns>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete]
    [Route("Leave/Room/{roomId}/{playerId}")]
    public IActionResult LeaveRoom(
        [RoomIdValidation]
        string roomId,
        [PlayerIdValidation]
        string playerId)
    {
        if (!_playerService.PlayerIdExists(roomId, playerId))
        {
            return BadRequest($"Player with id {playerId} does not exist in the room with id {roomId}!");
        }

        return Ok(_gameRoomService.RemovePlayer(roomId, playerId));
    }

    /// <summary>
    ///     Submits the vote of the specified player in the specified game room.
    /// </summary>
    /// <param name="roomId">The id of game room in which the voting process is happening</param>
    /// <param name="playerId">The id of player who has voted</param>
    /// <param name="vote">The vote value chosen</param>
    /// <response code="200">Success: Returns the updated game room object</response>
    /// <response code="400">Bad Request:
    /// if incorrectly entered roomId / playerId / vote or
    /// if room with the specified id does not exist or
    /// if player with the specified id does not exist in the specified room</response>
    /// <returns>Updated instance of game room.</returns>
    [ProducesResponseType(typeof(GameRoom), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut]
    [Route("Vote/{roomId}/{playerId}")]
    public IActionResult Vote(
        [RoomIdValidation]
        string roomId, 
        [PlayerIdValidation]
        string playerId, 
        [Required]
        PlayerVote vote)
    {
        if (!_playerService.PlayerIdExists(roomId, playerId))
        {
            return BadRequest($"Player with id {playerId} does not exist in the room with id {roomId}!");
        }
        
        return Ok(_playerService.Vote(roomId, playerId, vote));
    }
}