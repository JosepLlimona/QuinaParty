using Microsoft.AspNetCore.SignalR;
using QuinaPartyServer.Interfaces;
using QuinaPartyServer.Models;

namespace QuinaPartyServer.Hubs;

public class GameHub : Hub<IGameClient>
{

    private static List<Room> rooms = new();

    public async Task<string> CreateRoom()
    {
        Random res = new Random();

        string str = "abcdefghijklmnopqrstuvwxyz0123456789";

        string roomName = "";

        for (int i = 0; i < 6; i++)
        {
            int x = res.Next(str.Length);

            roomName = roomName + char.ToUpper(str[x]);
        }

        roomName.ToUpper();

        var newRoom = new Room
        {
            adminConnectionId = Context.ConnectionId,
            users = new List<User>(),
            RoomCode = roomName
        };

        rooms.Add(newRoom);

        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

        return roomName;
    }

    public async Task ConnectToRoom(string roomName, string userName)
    {
        if (!rooms.Any(r => r.RoomCode == roomName))
            return; //Enviar missatge error

        var user = new User
        {
            connectionId = Context.ConnectionId,
            Name = userName
        };

        var room = rooms.First(r => r.RoomCode == roomName);

        room.users.Add(user);

        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

        Console.WriteLine($"Connectat {user.Name}: {user.connectionId}");

        await Clients.Client(room.adminConnectionId).UserJoined(user.Name);
    }

    public async Task<bool> CheckIfAdminConnected(string rommName)
    {
        return rooms.FirstOrDefault(r => r.RoomCode == rommName)?.adminConnectionId == Context.ConnectionId;
    }

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("Connectat: " + Context.ConnectionId);
        await Clients.Caller.SendAsync("SystemMessage", "Connected to SignalR hub");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
