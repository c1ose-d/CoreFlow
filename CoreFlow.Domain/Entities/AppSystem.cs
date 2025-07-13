namespace CoreFlow.Domain.Entities;

public partial class AppSystem
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string ShortName { get; private set; } = null!;

    private readonly List<UserAppSystem> _userAppSystems = [];

    private AppSystem() { }

    public AppSystem(string name, string shortName)
    {
        ValidateName(name);
        ValidateShortName(shortName);

        Id = Guid.NewGuid();
        Name = name;
        ShortName = shortName;
    }

    public void Update(string? name = null, string? shortName = null)
    {
        if (name != null)
        {
            ValidateName(name);
            Name = name;
        }

        if (shortName != null)
        {
            ValidateShortName(shortName);
            ShortName = shortName;
        }
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 200)
        {
            throw new ArgumentException("Name required, ≤200 chars.", nameof(name));
        }
    }

    private static void ValidateShortName(string shortName)
    {
        if (string.IsNullOrWhiteSpace(shortName) || shortName.Length > 50)
        {
            throw new ArgumentException("Short name required, ≤50 chars.", nameof(shortName));
        }
    }
}