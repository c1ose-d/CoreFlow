namespace CoreFlow.Domain.Entities;

public partial class IdBlock
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Guid SystemId { get; private set; }

    private readonly List<SystemId> _systemIds = [];
    public IReadOnlyCollection<SystemId> SystemIds => _systemIds;

    private IdBlock() { }

    public IdBlock(string name, Guid systemId)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 200)
        {
            throw new ArgumentException("Name required, ≤200 chars.", nameof(name));
        }

        Id = Guid.NewGuid();
        Name = name;
        SystemId = systemId;
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 200)
        {
            throw new ArgumentException("Name required, ≤200 chars.", nameof(name));
        }

        Name = name;
    }
}