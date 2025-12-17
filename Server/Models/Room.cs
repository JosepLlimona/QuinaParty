namespace QuinaPartyServer.Models;

public class Room
{
    public string adminConnectionId { get; set; } = null!;

    public List<User> users { get; set; } = new();

    public string RoomCode { get; set; } = null!;
}

public sealed record User
{
    public string connectionId { get; set; } = null!;
    public string Name { get; set; } = null!;
}
