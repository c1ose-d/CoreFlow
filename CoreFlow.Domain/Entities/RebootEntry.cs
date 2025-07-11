namespace CoreFlow.Domain.Entities;

public partial class RebootEntry
{
    public Guid Id { get; set; }
    public Guid RebootId { get; set; }
    public Guid? ServerId { get; set; }
    public Guid? ServerBlockId { get; set; }

    public virtual Reboot Reboot { get; set; } = null!;
    public virtual Server? Server { get; set; }
    public virtual ServerBlock? ServerBlock { get; set; }
}