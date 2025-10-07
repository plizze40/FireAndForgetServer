namespace FireAndForget.Server.Network.Messages;

public class CreateActorMessage : MessageBase
{
    public CreateActorMessage()
    {
        Type = MessageType.CreateActor;
    }

    public string Name { get; set; } = string.Empty;
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}
