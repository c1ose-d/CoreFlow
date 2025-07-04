namespace CoreFlow.Persistence.Models;

[Table("server_blocks")]
[Index("Name", Name = "idx_server_blocks_name", IsUnique = true)]
public partial class ServerBlock
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [InverseProperty("ServerBlock")]
    public virtual ICollection<RebootList> RebootLists { get; set; } = [];

    [InverseProperty("Block")]
    public virtual ICollection<Server> Servers { get; set; } = [];
}