namespace FireAndForget.Server.Network.Messages;

public class DeleteActorMessage : MessageBase
{
    public DeleteActorMessage()
    {
        Type = MessageType.DeleteActor;
    }
    public int Id { get; set; }
}
