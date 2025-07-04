namespace CoreFlow.Persistence.Models;

[Table("reboot_commands")]
[Index("RebootId", "ExecutionOrder", Name = "idx_reboot_commands_order", IsUnique = true)]
[Index("RebootId", Name = "idx_reboot_commands_reboot_id")]
public partial class RebootCommand
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("command_text")]
    public string CommandText { get; set; } = null!;

    [Column("execution_order")]
    public int ExecutionOrder { get; set; }

    [Column("reboot_id")]
    public Guid RebootId { get; set; }

    [ForeignKey("RebootId")]
    [InverseProperty("RebootCommands")]
    public virtual Reboot Reboot { get; set; } = null!;
}