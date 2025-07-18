namespace CoreFlow.Domain.Entities;

public partial class ServerBlock
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Guid AppSystemId { get; private set; }

    private readonly List<Server> _servers = [];
    public IReadOnlyCollection<Server> Servers => _servers;

    private ServerBlock() { }

    public ServerBlock(string name, Guid appSystemId)
    {
        ValidateName(name);

        Id = Guid.NewGuid();
        Name = name;
        AppSystemId = appSystemId;
    }

    public void Update(string? name = null)
    {
        if (name != null)
        {
            ValidateName(name);
            Name = name;
        }
    }

    public void ChangeAppSystem(Guid? appSystemId = null)
    {
        if (appSystemId != null)
        {
            AppSystemId = (Guid)appSystemId;
        }
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 200)
        {
            throw new ArgumentException("Name required, ≤200 chars.", nameof(name));
        }
    }
}