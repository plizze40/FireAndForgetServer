using SuperSocket.WebSocket.Server;

namespace FireAndForget.Server.Network;

public class GameSession : WebSocketSession
{
    public int Id { get; set; }
    public bool FirstPacketRead { get; set; }
}
