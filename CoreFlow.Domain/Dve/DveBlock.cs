namespace CoreFlow.Domain.Dve;

public class DveBlock
{
    public Guid Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    private readonly List<DveId> _dveIds = [];
    public IReadOnlyCollection<DveId> DveIds => _dveIds.AsReadOnly();

    protected DveBlock() { }

    public DveBlock(Guid id, string name)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
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

    public void AddId(DveId dveId)
    {
        ArgumentNullException.ThrowIfNull(dveId, nameof(dveId));
        if (dveId.BlockId != Id)
        {
            throw new InvalidOperationException("BlockId mismatch");
        }

        if (_dveIds.Any(x => x.Name.Equals(dveId.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"An id with name '{dveId.Name}' already exists in this block");
        }

        _dveIds.Add(dveId);
    }

    public bool RemoveId(Guid dveId)
    {
        DveId? existing = _dveIds.FirstOrDefault(x => x.Id == dveId);
        return existing is not null && _dveIds.Remove(existing);
    }
}