namespace CoreFlow.Domain.Entities;

public partial class System
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = null!;

    private readonly List<UserSystem> _userSystems = [];
    public IReadOnlyCollection<UserSystem> UserSystems => _userSystems;

    private System() { }

    public System(string name, string shortName)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 200)
        {
            throw new ArgumentException("Name required, ≤200 chars.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(shortName) || shortName.Length > 50)
        {
            throw new ArgumentException("Short name required, ≤50 chars.", nameof(shortName));
        }

        Id = Guid.NewGuid();
        Name = name;
        ShortName = shortName;
    }

    public void Update(string? name = null, string? shortName = null)
    {
        if (name != null)
        {
            Name = name.Length <= 200 ? name : throw new ArgumentException("Name ≤200 chars", nameof(name));
        }

        if (shortName != null)
        {
            ShortName = shortName.Length <= 50 ? shortName : throw new ArgumentException("Short nameame ≤50 chars", nameof(shortName));
        }
    }
}