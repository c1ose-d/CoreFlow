namespace CoreFlow.Domain.Entities;

public partial class RebootCommand
{
    public Guid Id { get; set; }
    public string CommandText { get; set; } = null!;
    public int ExecutionOrder { get; set; }
    public Guid RebootId { get; set; }

    public virtual Reboot Reboot { get; set; } = null!;
}