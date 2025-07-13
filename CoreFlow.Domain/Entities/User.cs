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

    private readonly List<UserAppSystem> _userAppSystems = [];
    public IReadOnlyCollection<UserAppSystem> UserAppSystems => _userAppSystems;

    private User() { }

    public User(string lastName, string firstName, string? middleName, string userName, string password, bool isAdmin = false)
    {
        ValidateLastName(lastName);
        ValidateFirstName(firstName);
        if (middleName != null)
        {
            ValidateMiddleName(middleName);
        }
        ValidateUserName(userName);
        ValidatePassword(password);

        Id = Guid.NewGuid();
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        UserName = userName;
        Password = password;
        IsAdmin = isAdmin;
    }

    public void Update(string? lastName = null, string? firstName = null, string? middleName = null, string? userName = null)
    {
        if (lastName != null)
        {
            ValidateLastName(lastName);
            LastName = lastName;
        }

        if (firstName != null)
        {
            ValidateFirstName(firstName);
            FirstName = firstName;
        }

        if (middleName != null)
        {
            ValidateMiddleName(middleName);
            MiddleName = middleName;
        }

        if (userName != null)
        {
            ValidateUserName(userName);
            UserName = userName;
        }
    }

    public void ChangePassword(string password)
    {
        ValidatePassword(password);
        Password = password;
    }

    public void SetAdmin(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void AddAppSystem(AppSystem appSystem)
    {
        if (_userAppSystems.Any(userSystem => userSystem.AppSystemId == appSystem.Id))
        {
            return;
        }

        _userAppSystems.Add(new UserAppSystem(this, appSystem));
    }

    public void RemoveAppSystem(Guid appSystemId)
    {
        UserAppSystem? userAppSystem = _userAppSystems.FirstOrDefault(us => us.AppSystemId == appSystemId);

        if (userAppSystem != null)
        {
            _ = _userAppSystems.Remove(userAppSystem);
        }
    }

    private static void ValidateLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > 50)
        {
            throw new ArgumentException("Last name required, ≤50 chars.", nameof(lastName));
        }
    }

    private static void ValidateFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > 50)
        {
            throw new ArgumentException("First name required, ≤50 chars.", nameof(firstName));
        }
    }

    private static void ValidateMiddleName(string middleName)
    {
        if (string.IsNullOrWhiteSpace(middleName) || middleName.Length > 50)
        {
            throw new ArgumentException("Middle name ≤50 chars.", nameof(middleName));
        }
    }

    private static void ValidateUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName) || userName.Length > 50)
        {
            throw new ArgumentException("User name required, ≤50 chars.", nameof(userName));
        }
    }

    private static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length > 50)
        {
            throw new ArgumentException("Password required, ≤50 chars.", nameof(password));
        }
    }
}