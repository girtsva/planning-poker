using PlanningPoker.Models;

namespace PlanningPoker.Services.Interfaces;

public interface IGameRoomService
{
    /// <summary>
    ///     Creates a new game room in the data repository.
    /// </summary>
    /// <param name="roomName">The desired name of the game room.</param>
    public GameRoom CreateGameRoom(string roomName);

    /// <summary>
    ///     Returns a list of created game rooms in the data repository.
    /// </summary>
    /// <returns>List of created game rooms if found; otherwise empty list.</returns>
    public ICollection<GameRoom> ListGameRooms();

    /// <summary>
    ///     Searches and returns game room from data repository by given room name.
    /// </summary>
    /// <param name="roomName"></param>
    /// <returns>Instance of found game room if found; otherwise <c>null</c>.</returns>
    public GameRoom? GetGameRoomByName(string roomName);

    // public void AddPlayer(Player name);
    /// <summary>
    ///     Lists players in the game room.
    /// </summary>
    /// <returns>List of players if any; otherwise empty list.</returns>
    public ICollection<Player> ListUsers();

    /// <summary>
    ///     Checks whether game room with specified name exists in the data repository.
    /// </summary>
    /// <param name="roomName">Specified game room name.</param>
    /// <returns><c>true</c> if game room exists; otherwise <c>false</c>.</returns>
    public bool RoomNameExists(string roomName);

    /// <summary>
    ///     Deletes all game rooms from the data repository.
    /// </summary>
    public void DeleteAllRooms();

    /// <summary>
    ///     Deletes the specified game room by room name from the data repository.
    /// </summary>
    /// <param name="roomName">Specified game room name.</param>
    public void DeleteRoom(string roomName);
}