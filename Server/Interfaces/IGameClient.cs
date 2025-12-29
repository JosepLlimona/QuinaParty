namespace QuinaPartyServer.Interfaces;


// Aqui van les funcions del client
public interface IGameClient
{
    Task SendAsync(string user, string message);
    Task StartGame(string roomName);

    Task UserJoined(string userName);
}
