namespace CoreFlow.Domain.Entities;

public partial class UserAppSystem
{
    public Guid UserId { get; private set; }
    public Guid AppSystemId { get; private set; }

    public virtual User User { get; private set; } = null!;
    public virtual AppSystem AppSystem { get; private set; } = null!;

    private UserAppSystem() { }

    public UserAppSystem(User user, AppSystem appSystem)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        AppSystem = appSystem ?? throw new ArgumentNullException(nameof(appSystem));

        UserId = user.Id;
        AppSystemId = appSystem.Id;
    }
}