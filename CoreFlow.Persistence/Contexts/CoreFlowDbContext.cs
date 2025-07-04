namespace CoreFlow.Persistence.Contexts;

public partial class CoreFlowDbContext : DbContext
{
    public CoreFlowDbContext() { }

    public CoreFlowDbContext(DbContextOptions<CoreFlowDbContext> options) : base(options) { }

    public virtual DbSet<DveBlock> DveBlocks { get; set; } = null!;

    public virtual DbSet<DveId> DveIds { get; set; } = null!;

    public virtual DbSet<LinuxBlock> LinuxBlocks { get; set; } = null!;

    public virtual DbSet<LinuxCommand> LinuxCommands { get; set; } = null!;

    public virtual DbSet<Reboot> Reboots { get; set; } = null!;

    public virtual DbSet<RebootCommand> RebootCommands { get; set; } = null!;

    public virtual DbSet<RebootList> RebootLists { get; set; } = null!;

    public virtual DbSet<Server> Servers { get; set; } = null!;

    public virtual DbSet<ServerBlock> ServerBlocks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<DveBlock>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("dve_blocks_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();
        });

        _ = modelBuilder.Entity<DveId>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("dve_ids_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Block).WithMany(p => p.DveIds).HasConstraintName("fk_dve_block");
        });

        _ = modelBuilder.Entity<LinuxBlock>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("linux_blocks_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();
        });

        _ = modelBuilder.Entity<LinuxCommand>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("linux_commands_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Block).WithMany(p => p.LinuxCommands).HasConstraintName("fk_linux_block");
        });

        _ = modelBuilder.Entity<Reboot>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("reboots_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();
        });

        _ = modelBuilder.Entity<RebootCommand>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("reboot_commands_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Reboot).WithMany(p => p.RebootCommands).HasConstraintName("fk_reboot");
        });

        _ = modelBuilder.Entity<RebootList>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("reboot_lists_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Reboot).WithMany(p => p.RebootLists).HasConstraintName("fk_reboot_list");

            _ = entity.HasOne(d => d.ServerBlock).WithMany(p => p.RebootLists).HasConstraintName("fk_reboot_block");

            _ = entity.HasOne(d => d.Server).WithMany(p => p.RebootLists).HasConstraintName("fk_reboot_server");
        });

        _ = modelBuilder.Entity<Server>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("servers_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.HasOne(d => d.Block).WithMany(p => p.Servers).HasConstraintName("fk_server_block");
        });

        _ = modelBuilder.Entity<ServerBlock>(entity =>
        {
            _ = entity.HasKey(e => e.Id).HasName("server_blocks_pkey");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}