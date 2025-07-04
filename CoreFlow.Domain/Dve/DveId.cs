namespace CoreFlow.Domain.Dve;

public class DveId
{
    public Guid Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Content { get; private set; } = default!;
    public Guid BlockId { get; private set; }

    protected DveId() { }

    public DveId(Guid id, string name, string content, Guid blockId)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(content, nameof(content));
        Id = id;
        Name = name;
        Content = content;
        BlockId = blockId;
    }

    public void Update(string newName, string newContent)
    {
        ArgumentNullException.ThrowIfNull(newName, nameof(newName));
        ArgumentNullException.ThrowIfNull(newContent, nameof(newContent));
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("Name cannot be empty", nameof(newName));
        }

        if (string.IsNullOrWhiteSpace(newContent))
        {
            throw new ArgumentException("Content cannot be empty", nameof(newContent));
        }

        Name = newName;
        Content = newContent;
    }
}