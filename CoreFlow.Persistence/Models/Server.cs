namespace CoreFlow.Persistence.Models;

[Table("servers")]
[Index("BlockId", Name = "idx_servers_block_id")]
[Index("IpAddress", Name = "idx_servers_ip", IsUnique = true)]
public partial class Server
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("ip_address")]
    [StringLength(100)]
    public string IpAddress { get; set; } = null!;

    [Column("host_name")]
    [StringLength(200)]
    public string? HostName { get; set; }

    [Column("block_id")]
    public Guid BlockId { get; set; }

    [ForeignKey("BlockId")]
    [InverseProperty("Servers")]
    public virtual ServerBlock Block { get; set; } = null!;

    [InverseProperty("Server")]
    public virtual ICollection<RebootList> RebootLists { get; set; } = [];
}