using FireAndForget.Server.Game;
using FireAndForget.Server.Services;
using SuperSocket.Connection;
using SuperSocket.WebSocket;

namespace FireAndForget.Server.Network;

public class NetworkManager(World world, DispatcherService dispatcher)
{
    public ValueTask OnSessionConnectedAsync(GameSession session)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask OnHandleMessageAsync(GameSession session, WebSocketPackage package)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask OnSessionClosed(GameSession session, CloseEventArgs args)
    {
        return ValueTask.CompletedTask;
    }
}