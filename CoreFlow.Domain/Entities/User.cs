namespace CoreFlow.Domain.Entities;

public partial class User
{
    public Guid Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsAdmin { get; set; }

    private readonly List<UserSystem> _userSystems = [];
    public IReadOnlyCollection<UserSystem> UserSystems => _userSystems.AsReadOnly();

    private User() { }

    public User(string lastName, string firstName, string? middleName, string userName, string password, bool isAdmin = false)
    {
        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > 50)
        {
            throw new ArgumentException("Last name required, ≤50 chars.", nameof(lastName));
        }

        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > 50)
        {
            throw new ArgumentException("First name required, ≤50 chars.", nameof(firstName));
        }

        if (!string.IsNullOrWhiteSpace(middleName) && middleName.Length > 50)
        {
            throw new ArgumentException("Middle name ≤50 chars.", nameof(middleName));
        }

        if (string.IsNullOrWhiteSpace(userName) || userName.Length > 50)
        {
            throw new ArgumentException("User name required, ≤50 chars.", nameof(userName));
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length > 50)
        {
            throw new ArgumentException("Password required, ≤50 chars.", nameof(password));
        }

        Id = Guid.NewGuid();
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        UserName = userName;
        Password = password;
        IsAdmin = isAdmin;
    }

    public void ChangeUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName) || userName.Length > 50)
        {
            throw new ArgumentException("User name required, ≤50 chars.", nameof(userName));
        }

        UserName = userName;
    }

    public void ChangePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length > 50)
        {
            throw new ArgumentException("Password required, ≤50 chars.", nameof(password));
        }

        Password = password;
    }

    public void SetAdmin(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void AddSystem(Guid systemId)
    {
        if (_userSystems.Any(userSystem => userSystem.SystemId == systemId))
        {
            return;
        }

        _userSystems.Add(new UserSystem(Id, systemId));
    }

    public void RemoveSystem(Guid systemId)
    {
        UserSystem? userSystem = _userSystems.FirstOrDefault(us => us.SystemId == systemId);

        if (userSystem != null)
        {
            _ = _userSystems.Remove(userSystem);
        }
    }
}