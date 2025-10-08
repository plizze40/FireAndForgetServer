using Microsoft.Extensions.DependencyInjection;
using SuperSocket.Server;
using SuperSocket.WebSocket;
using SuperSocket.WebSocket.Server;

namespace FireAndForget.Server.Network;

public static class NetworkServiceExtensions
{
    public static IServiceCollection AddNetworkManager(this IServiceCollection services)
    {
        services.AddTransient<NetworkManager>();
        services.AddTransient<SessionHandlers>(sp =>
        {
            NetworkManager handler = sp.GetRequiredService<NetworkManager>();
            return new SessionHandlers
            {
                Connected = session => handler.OnSessionConnectedAsync((GameSession)session),
                Closed = (session, args) => handler.OnSessionClosed((GameSession)session, args)
            };
        });

        services.AddTransient<Func<WebSocketSession, WebSocketPackage, ValueTask>>(sp =>
        {
            return async (session, package) => await sp.GetRequiredService<NetworkManager>()
                .OnHandleMessageAsync((GameSession)session, package);
        });
        
        return services;
    }
}