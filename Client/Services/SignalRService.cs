using Microsoft.AspNetCore.SignalR.Client;

namespace Client.Services;

public class SignalRService
{
    public HubConnection Connection { get; private set; } = null!;

    public bool IsConnected => Connection?.State == HubConnectionState.Connected;

    private readonly Dictionary<string, Delegate> _eventHandlers = new Dictionary<string, Delegate>();

    public async Task StartConnectionAsync(string hubUrl)
    {
        if (Connection is null)
        {

            Connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();
        }

        if (Connection.State == HubConnectionState.Disconnected)
        {
            await Connection.StartAsync();
        }
    }

    public async Task StopConnectionAsync()
    {
        if (Connection is not null && Connection.State == HubConnectionState.Connected)
            await Connection.StopAsync();
    }
}
