namespace CoreFlow.Domain.Reboot;

public class RebootListEntry
{
    public Guid Id { get; private set; } = default!;
    public Guid RebootId { get; private set; }
    public Guid? ServerId { get; private set; }
    public Guid? ServerBlockId { get; private set; }

    protected RebootListEntry() { }

    public RebootListEntry(Guid id, Guid rebootId, Guid? serverId, Guid? serverBlockId)
    {
        Id = id;
        RebootId = rebootId;
        ServerId = serverId;
        ServerBlockId = serverBlockId;

        if (ServerId is null && ServerBlockId is null)
            throw new InvalidOperationException("Target must be a server or server block");
    }
}