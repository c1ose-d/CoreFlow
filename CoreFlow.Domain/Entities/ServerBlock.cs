namespace CoreFlow.Domain.Entities;

public partial class ServerBlock
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid SystemId { get; set; }

    public virtual ICollection<RebootEntry> RebootEntries { get; set; } = [];

    public virtual ICollection<Server> Servers { get; set; } = [];
}