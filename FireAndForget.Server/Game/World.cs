using FireAndForget.Server.Network;

namespace FireAndForget.Server.Game;

public record struct Position(int X, int Y);

public sealed class World
{
    private List<Player> _players = new();

    public const int Width = 10;
    public const int Height = 10;

    public bool[,] Walkable { get; set; } = new bool[Width, Height];

    public World()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (x > 0 && x < Width - 1 && y > 0 && y < Height - 1)
                {
                    Walkable[x, y] = true;
                }
                else
                {
                    Walkable[x, y] = false;
                }
            }
        }

        Walkable[5, 5] = false;
    }

    public bool IsWalkable(Position pos)
    {
        //if (GetActorAt(pos) != null)
        //    return false;

        if (pos.X < 0 || pos.X >= Width || pos.Y < 0 || pos.Y >= Height)
            return false;

        return Walkable[pos.X, pos.Y];
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
