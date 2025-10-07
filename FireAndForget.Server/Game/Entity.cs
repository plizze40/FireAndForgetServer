namespace FireAndForget.Server.Game;

public abstract class Entity
{
    public int Id { get; set; }
    public Position Position { get; set; }

    public void Update(TimeSpan tickrate)
    {

    }
}
