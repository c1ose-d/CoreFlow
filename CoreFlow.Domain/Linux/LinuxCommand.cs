namespace CoreFlow.Domain.Linux;

public class LinuxCommand
{
    public Guid Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Content { get; private set; } = default!;
    public Guid BlockId { get; private set; }

    protected LinuxCommand() { }

    public LinuxCommand(Guid id, string name, string content, Guid blockId)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(content, nameof(content));

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Content cannot be empty", nameof(content));
        }

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