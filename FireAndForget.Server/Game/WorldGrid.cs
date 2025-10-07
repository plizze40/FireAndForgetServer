namespace FireAndForget.Server.Game;

public class Tile
{
    public Entity? Entity { get; set; }
    public bool Blocked { get; set; }
}

public class WorldGrid
{
    public int Width { get; init; }
    public int Height { get; init; }
    public Tile[,] Tiles { get; private set; }

    public WorldGrid(int width, int height)
    {
        Width = width;
        Height = height;
        
        Tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tiles[x, y] = new Tile();
            }
        }
    }
    
    public bool CanMoveTo(Position pos)
    {
        if (!InBounds(pos))
        {
            return false;
        }

        if (Tiles[pos.X, pos.Y].Blocked)
        {
            return false;
        }
        
        return Tiles[pos.X, pos.Y].Entity == null;
    }
    
    public void PlaceEntity(Entity entity, Position pos)
    {
        if (!InBounds(pos))
        {
            return;
        }
        
        Tiles[pos.X, pos.Y].Entity = entity;
        entity.Position = pos;
    }

    public void RemoveEntity(Position pos)
    {
        if (InBounds(pos))
        {
            Tiles[pos.X, pos.Y].Entity = null;
        }
    }
    
    public bool InBounds(Position pos)
    {
        return pos.X >= 0 && pos.X < Width &&
               pos.Y >= 0 && pos.Y < Height;
    }
    
    public void BlockTile(Position pos)
    {
        if (InBounds(pos))
        {
            Tiles[pos.X, pos.Y].Blocked = true;
        }
    }

    public void UnblockTile(Position pos)
    {
        if (InBounds(pos))
        {
            Tiles[pos.X, pos.Y].Blocked = false;
        }
    }
 
    public void ToggleBlock(Position pos)
    {
        if (InBounds(pos))
        {
            Tiles[pos.X, pos.Y].Blocked = !Tiles[pos.X, pos.Y].Blocked;
        }
    }
}