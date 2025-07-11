namespace CoreFlow.Domain.Entities;

public partial class UserSystem
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SystemId { get; set; }

    public virtual System System { get; set; } = null!;
    public virtual User User { get; set; } = null!;

    private UserSystem() { }

    public UserSystem(Guid userId, Guid systemId)
    {
        UserId = userId;
        SystemId = systemId;
    }
}