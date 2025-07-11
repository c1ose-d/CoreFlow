namespace CoreFlow.Domain.Entities;

public partial class Reboot
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid SystemId { get; set; }

    public virtual ICollection<RebootCommand> RebootCommands { get; set; } = [];
    public virtual ICollection<RebootEntry> RebootEntries { get; set; } = [];
}