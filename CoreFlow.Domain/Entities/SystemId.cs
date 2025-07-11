namespace CoreFlow.Domain.Entities;

public partial class SystemId
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Content { get; set; } = null!;
    public Guid BlockId { get; set; }

    public virtual IdBlock Block { get; set; } = null!;
}