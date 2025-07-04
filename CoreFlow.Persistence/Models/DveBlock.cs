namespace CoreFlow.Persistence.Models;

[Table("dve_blocks")]
[Index("Name", Name = "idx_dve_blocks_name", IsUnique = true)]
public partial class DveBlock
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [InverseProperty("Block")]
    public virtual ICollection<DveId> DveIds { get; set; } = [];
}