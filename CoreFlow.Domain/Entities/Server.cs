namespace CoreFlow.Domain.Entities;

public partial class Server
{
    public Guid Id { get; set; }
    public string IpAddress { get; set; } = null!;
    public string? HostName { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Guid BlockId { get; set; }

    public virtual ServerBlock Block { get; set; } = null!;

    public virtual ICollection<RebootEntry> RebootEntries { get; set; } = [];
}