using Microsoft.AspNetCore.SignalR;

namespace Auth.Application.SignalR;

public class AuthHub:Hub<ISignalHubClient>
{
    public static string? ConnectionId { get;private set; }

    public override Task OnConnectedAsync()
    {
        ConnectionId = Context.ConnectionId;
        
        Clients.Client(ConnectionId).SendServerTimeAsync(DateTime.Now);

        return base.OnConnectedAsync();
    }
}
public interface ISignalHubClient
{
    public Task SendServerTimeAsync(DateTime date);
}