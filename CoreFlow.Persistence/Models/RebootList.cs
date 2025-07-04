namespace CoreFlow.Persistence.Models;

[Table("reboot_lists")]
[Index("ServerBlockId", Name = "idx_reboot_lists_block_id")]
[Index("RebootId", Name = "idx_reboot_lists_reboot_id")]
[Index("ServerId", Name = "idx_reboot_lists_server_id")]
public partial class RebootList
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("reboot_id")]
    public Guid RebootId { get; set; }

    [Column("server_id")]
    public Guid? ServerId { get; set; }

    [Column("server_block_id")]
    public Guid? ServerBlockId { get; set; }

    [ForeignKey("RebootId")]
    [InverseProperty("RebootLists")]
    public virtual Reboot Reboot { get; set; } = null!;

    [ForeignKey("ServerId")]
    [InverseProperty("RebootLists")]
    public virtual Server? Server { get; set; }

    [ForeignKey("ServerBlockId")]
    [InverseProperty("RebootLists")]
    public virtual ServerBlock? ServerBlock { get; set; }
}