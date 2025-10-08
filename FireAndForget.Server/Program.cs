using FireAndForget.Server.Game;
using FireAndForget.Server.Network;
using FireAndForget.Server.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperSocket.WebSocket.Server;

namespace FireAndForget.Server;

internal static class Program
{
    private static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.AddSingleton<World>();
        services.AddSingleton<DispatcherService>();
        services.AddNetworkManager();

        services.AddHostedService<GameLoopService>();
    }

    private static async Task Main(string[] args)
    {
        IHost host = WebSocketHostBuilder.Create()
            .UseSession<GameSession>()
            .ConfigureServices(Configure)
            .ConfigureAppConfiguration((hostCtx, configApp) =>
            {
                configApp.AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "serverOptions:name", "TestServer" },
                    { "serverOptions:listeners:0:ip", "Any" },
                    { "serverOptions:listeners:0:port", "3000" }
                }!);
            })
            .ConfigureLogging((hostCtx, loggingBuilder) =>
            {
                loggingBuilder.AddConsole();
            })
            .Build();

        await host.RunAsync();
    }
}