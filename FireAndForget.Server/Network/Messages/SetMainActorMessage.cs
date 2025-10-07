namespace FireAndForget.Server.Network.Messages;

public class SetMainActorMessage : MessageBase
{
    public SetMainActorMessage()
    {
        Type = MessageType.SetMainActor;
    }
    public int Id { get; set; }
}