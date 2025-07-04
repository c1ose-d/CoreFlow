namespace CoreFlow.Persistence.Models;

[Table("linux_blocks")]
[Index("Name", Name = "idx_linux_blocks_name", IsUnique = true)]
public partial class LinuxBlock
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [InverseProperty("Block")]
    public virtual ICollection<LinuxCommand> LinuxCommands { get; set; } = [];
}