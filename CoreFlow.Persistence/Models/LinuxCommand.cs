namespace CoreFlow.Persistence.Models;

[Table("linux_commands")]
[Index("BlockId", Name = "idx_linux_commands_block_id")]
public partial class LinuxCommand
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
    [InverseProperty("LinuxCommands")]
    public virtual LinuxBlock Block { get; set; } = null!;
}