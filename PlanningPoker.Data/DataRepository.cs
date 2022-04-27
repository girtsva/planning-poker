﻿using PlanningPoker.Data.Interfaces;
using PlanningPoker.Models;

namespace PlanningPoker.Data;

//[UsedImplicitly]
public class DataRepository : IDataRepository
{
    //public static ICollection<Player> Players = new List<Player>();
    private static readonly IDictionary<string, GameRoom> GameRooms = new Dictionary<string, GameRoom>();

    public GameRoom CreateGameRoom(string roomName)
    {
        var gameRoom = new GameRoom(roomName);
        GameRooms.Add(gameRoom.Id, gameRoom);
        return gameRoom;
    }

    public ICollection<GameRoom> ListGameRooms()
    {
        return GameRooms.Values;
    }

    public GameRoom? GetGameRoomById(string roomId)
    {
        return GameRooms.ContainsKey(roomId) ? GameRooms[roomId] : null;
    }

    public GameRoom? AddPlayer(string roomId, Player player)
    {
        var gameRoom = GameRooms[roomId];
        gameRoom?.Players.Add(player);
        return gameRoom;
    }

    public ICollection<Player> ListUsers()
    {
        var result = GameRooms.Values.SelectMany(gameRoom =>
            gameRoom.Players);
        return result.ToList();
    }

    public bool RoomNameExists(string roomName)
    {
        return GameRooms.Values.Any(gameRoom => gameRoom.Name == roomName);
    }
    
    public bool RoomIdExists(string roomId)
    {
        return GameRooms.ContainsKey(roomId);
    }

    public void DeleteAllRooms()
    {
        GameRooms.Clear();
    }

    public void DeleteRoom(string roomId)
    {
        GameRooms.Remove(roomId);
    }
}