namespace CoreFlow.Domain.Entities;

public partial class CommandBlock
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid SystemId { get; set; }

    public virtual ICollection<Command> Commands { get; set; } = [];
}