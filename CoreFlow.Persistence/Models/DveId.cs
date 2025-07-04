namespace CoreFlow.Persistence.Models;

[Table("dve_ids")]
[Index("BlockId", Name = "idx_dve_ids_block_id")]
[Index("BlockId", "Name", Name = "idx_dve_ids_block_name", IsUnique = true)]
public partial class DveId
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [Column("content")]
    public string Content { get; set; } = null!;

    [Column("block_id")]
    public Guid BlockId { get; set; }

    [ForeignKey("BlockId")]
    [InverseProperty("DveIds")]
    public virtual DveBlock Block { get; set; } = null!;
}