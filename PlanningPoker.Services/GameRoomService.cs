using PlanningPoker.Data;
using PlanningPoker.Data.Interfaces;
using PlanningPoker.Models;
using PlanningPoker.Services.Interfaces;

namespace PlanningPoker.Services;

public class GameRoomService : IGameRoomService
{
    //private readonly GameRoom _gameRoom;
    private readonly IDataRepository _dataRepository;

    public GameRoomService(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
        //_gameRoom = gameRoom;
    }

    public void CreateGameRoom(string roomName, GameRoom room)
    {
        _dataRepository.CreateGameRoom(roomName, room);
    }

    public ICollection<GameRoom> ListGameRooms()
    {
        return _dataRepository.ListGameRooms();
    }
    
    public GameRoom? GetGameRoomByName(string roomName)
    {
        return _dataRepository.GetGameRoomByName(roomName);
    }

    // public void AddPlayer(Player name)
    // {
    //     _gameRoom.Players.Add(name);
    // }

    public ICollection<Player> ListUsers()
    {
        throw new NotImplementedException();
    }

    public bool RoomNameExists(string roomName)
    {
        return _dataRepository.RoomNameExists(roomName);
    }

    public void DeleteAllRooms()
    {
        _dataRepository.DeleteAllRooms();
    }

    public void DeleteRoom(string roomName)
    {
        _dataRepository.DeleteRoom(roomName);
    }
}