namespace CoreFlow.Persistence.Models;

[Table("reboots")]
[Index("Name", Name = "idx_reboots_name", IsUnique = true)]
public partial class Reboot
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [InverseProperty("Reboot")]
    public virtual ICollection<RebootCommand> RebootCommands { get; set; } = [];

    [InverseProperty("Reboot")]
    public virtual ICollection<RebootList> RebootLists { get; set; } = [];
}