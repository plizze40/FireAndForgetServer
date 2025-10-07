using System.Text.Json;
using FireAndForget.Server.Game;
using FireAndForget.Server.Network;
using FireAndForget.Server.Network.Messages;
using FireAndForget.Server.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperSocket.Server.Host;
using SuperSocket.WebSocket.Server;

namespace FireAndForget.Server;

internal class Program
{
    private static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.AddSingleton<World>();
        services.AddSingleton<DispatcherService>();

        services.AddHostedService<GameLoopService>();
    }

    private static async Task Main(string[] args)
    {
        IHost host = WebSocketHostBuilder.Create()
            .UseSession<GameSession>()
            .UseWebSocketMessageHandler(
                static async (session, message) =>
                {
                    await session.SendAsync(message.Message);
                }
            )
            .UseSessionHandler(static async (session) =>
            {
                WebSocketSession websocket = (WebSocketSession)session;

                await websocket.SendAsync(JsonSerializer.Serialize(new CreateActorMessage
                {
                    Id = 1,
                    Name = "Player1",
                    X = 3,
                    Y = 3
                }));

                await websocket.SendAsync(JsonSerializer.Serialize(new CreateActorMessage
                {
                    Id = 2,
                    Name = "Player1",
                    X = 3,
                    Y = 5
                }));

                await websocket.SendAsync(JsonSerializer.Serialize(new CreateActorMessage
                {
                    Id = 3,
                    Name = "Player1",
                    X = 3,
                    Y = 4
                }));

                await websocket.SendAsync(JsonSerializer.Serialize(new CreateActorMessage
                {
                    Id = 4,
                    Name = "Player1",
                    X = 4,
                    Y = 3
                }));

                await websocket.SendAsync(JsonSerializer.Serialize(new SetMainActorMessage
                {
                    Id = 1
                }));

                await Task.Delay(7000);

                await websocket.SendAsync(JsonSerializer.Serialize(new DeleteActorMessage
                {
                    Id = 3
                }));
            })
            .ConfigureServices(Configure)
            .ConfigureAppConfiguration((hostCtx, configApp) =>
            {
                configApp.AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "serverOptions:name", "TestServer" },
                    { "serverOptions:listeners:0:ip", "Any" },
                    { "serverOptions:listeners:0:port", "3000" }
                });
            })
            .ConfigureLogging((hostCtx, loggingBuilder) =>
            {
                loggingBuilder.AddConsole();
            })
            .Build();

        await host.RunAsync();
    }
}