namespace CoreFlow.Domain.Reboot;

public class Reboot
{
    public Guid Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    private readonly List<RebootCommand> _commands = [];
    public IReadOnlyCollection<RebootCommand> Commands => _commands.AsReadOnly();

    private readonly List<RebootListEntry> _targets = [];
    public IReadOnlyCollection<RebootListEntry> Targets => _targets.AsReadOnly();

    protected Reboot() { }

    public Reboot(Guid id, string name)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        Id = id;
        Name = name;
    }

    public void Rename(string newName)
    {
        ArgumentNullException.ThrowIfNull(newName, nameof(newName));
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("Name cannot be empty", nameof(newName));
        }

        Name = newName;
    }

    public void AddCommand(RebootCommand command)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        if (command.RebootId != Id)
        {
            throw new InvalidOperationException("RebootId mismatch");
        }

        if (_commands.Any(c => c.ExecutionOrder == command.ExecutionOrder))
        {
            throw new InvalidOperationException($"Command with order {command.ExecutionOrder} already exists");
        }

        _commands.Add(command);
    }

    public void AddTarget(RebootListEntry target)
    {
        ArgumentNullException.ThrowIfNull(target, nameof(target));

        if (target.RebootId != Id)
        {
            throw new InvalidOperationException("RebootId mismatch");
        }

        _targets.Add(target);
    }
}