namespace CoreFlow.Domain.Entities;

public partial class Command
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Content { get; set; } = null!;
    public Guid BlockId { get; set; }

    public virtual CommandBlock Block { get; set; } = null!;
}