namespace CoreFlow.Domain.Linux;

public class LinuxBlock
{
    public Guid Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    private readonly List<LinuxCommand> _commands = [];
    public IReadOnlyCollection<LinuxCommand> Commands => _commands.AsReadOnly();

    protected LinuxBlock() { }

    public LinuxBlock(Guid id, string name)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        Id = id;
        Name = name;
    }

    public void AddCommand(LinuxCommand command)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        if (command.BlockId != Id)
        {
            throw new InvalidOperationException("BlockId mismatch");
        }

        if (_commands.Any(x => x.Name.Equals(command.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"Command '{command.Name}' already exists in this block");
        }

        _commands.Add(command);
    }

    public bool RemoveCommand(Guid commandId)
    {
        LinuxCommand? existing = _commands.FirstOrDefault(x => x.Id == commandId);
        return existing is not null && _commands.Remove(existing);
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
}