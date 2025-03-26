using Auth.Application.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auth.Application.BackgroundServices;

public class TimeService(IServiceProvider sp) : BackgroundService
{

    public async Task DoAsync(CancellationToken cancellationToken)
    {
        using var scope = sp.CreateScope();

        var hubContext =scope.ServiceProvider.GetRequiredService<IHubContext<AuthHub, ISignalHubClient>>();
        while (true)
        {
            _ =hubContext.Clients.All.SendServerTimeAsync(DateTime.Now);
            await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);

        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return DoAsync(stoppingToken);
    }
}
