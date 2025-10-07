using FireAndForget.Server.Network;

namespace FireAndForget.Server.Game;

public class Player : Entity
{


    public string Name { get; set; } = string.Empty;
    public int Id { get; set; }
    public Position Position { get; set; }

    public Player(GameSession gameSession, World world)
    {
        
    }
}
