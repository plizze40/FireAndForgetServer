using FireAndForget.Server.Game;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace FireAndForget.Server.Services;

public class GameLoopService : BackgroundService
{
    private readonly World _world;
    private readonly DispatcherService _dispatcher;

    public GameLoopService(World world, DispatcherService dispatcher)
    {
        _world = world;
        _dispatcher = dispatcher;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        TimeSpan tickRate = TimeSpan.FromMilliseconds(50);
        Stopwatch stopwatch = Stopwatch.StartNew();
        TimeSpan accumulated = TimeSpan.Zero;

        PeriodicTimer timer = new(tickRate);

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            accumulated += stopwatch.Elapsed;
            stopwatch.Restart();

            while (accumulated >= tickRate)
            {
                await _dispatcher.FlushPendingAsync();
                _world.Update(tickRate);
                accumulated -= tickRate;
            }
        }
    }
}
