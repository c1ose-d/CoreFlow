namespace CoreFlow.Domain.Reboot;

public class RebootCommand
{
    public Guid Id { get; private set; } = default!;
    public string CommandText { get; private set; } = default!;
    public int ExecutionOrder { get; private set; }
    public Guid RebootId { get; private set; }

    protected RebootCommand() { }

    public RebootCommand(Guid id, string commandText, int executionOrder, Guid rebootId)
    {
        ArgumentNullException.ThrowIfNull(commandText, nameof(commandText));
        if (string.IsNullOrWhiteSpace(commandText))
        {
            throw new ArgumentException("Command text cannot be empty", nameof(commandText));
        }

        ArgumentOutOfRangeException.ThrowIfNegative(executionOrder);

        Id = id;
        CommandText = commandText;
        ExecutionOrder = executionOrder;
        RebootId = rebootId;
    }

    public void UpdateCommand(string newText)
    {
        ArgumentNullException.ThrowIfNull(newText, nameof(newText));
        if (string.IsNullOrWhiteSpace(newText))
        {
            throw new ArgumentException("Command text cannot be empty", nameof(newText));
        }

        CommandText = newText;
    }
}