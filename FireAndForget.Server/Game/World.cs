using FireAndForget.Server.Network;

namespace FireAndForget.Server.Game;

public record struct Position(int X, int Y);

public sealed class World
{
    private readonly List<Player> _players = new();

    public const int Width = 10;
    public const int Height = 10;
    
    public WorldGrid Grid { get; private set; }
    
    public World()
    {
        Grid = new WorldGrid(Width, Height);
        
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (x > 0 && x < Width - 1 && y > 0 && y < Height - 1)
                {
                    Grid.UnblockTile(new Position(x, y));
                }
                else
                {
                    Grid.BlockTile(new Position(x, y));
                }
            }
        }
        
        Grid.BlockTile(new Position(5, 5)); 
    }

    public void Update(TimeSpan tickRate)
    {
        _players.ForEach(p => p.Update(tickRate));
    }

    public void OnPlayerJoined(GameSession session)
    {
        
    }

    public void OnPlayerLeft(GameSession session)
    {

    }
}
