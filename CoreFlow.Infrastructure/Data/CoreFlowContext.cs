namespace CoreFlow.Infrastructure.Data;

public partial class CoreFlowContext(DbContextOptions<CoreFlowContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<AppSystem> AppSystems => Set<AppSystem>();
    public DbSet<ServerBlock> ServerBlocks => Set<ServerBlock>();
    public DbSet<Server> Servers => Set<Server>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreFlowContext).Assembly);

        //modelBuilder.Entity<Command>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("command_pkey");

        //    entity.ToTable("command");

        //    entity.HasIndex(e => e.BlockId, "idx_command_block_id");

        //    entity.HasIndex(e => new { e.BlockId, e.Name }, "idx_command_block_name").IsUnique();

        //    entity.Property(e => e.Id)
        //        .HasDefaultValueSql("gen_random_uuid()")
        //        .ValueGeneratedOnAdd()
        //        .HasColumnName("id");
        //    entity.Property(e => e.BlockId).HasColumnName("block_id");
        //    entity.Property(e => e.Content).HasColumnName("content");
        //    entity.Property(e => e.Name)
        //        .HasMaxLength(200)
        //        .HasColumnName("name");

        //    entity.HasOne(d => d.Block).WithMany(p => p.Commands)
        //        .HasForeignKey(d => d.BlockId)
        //        .HasConstraintName("fk_command_block");
        //});

        //modelBuilder.Entity<CommandBlock>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("command_block_pkey");

        //    entity.ToTable("command_block");

        //    entity.HasIndex(e => new { e.Name, e.SystemId }, "idx_command_block_name_system").IsUnique();

        //    entity.Property(e => e.Id)
        //        .HasDefaultValueSql("gen_random_uuid()")
        //        .ValueGeneratedOnAdd()
        //        .HasColumnName("id");
        //    entity.Property(e => e.Name)
        //        .HasMaxLength(200)
        //        .HasColumnName("name");
        //    entity.Property(e => e.SystemId).HasColumnName("system_id");
        //});

        //modelBuilder.Entity<IdBlock>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("id_block_pkey");

        //    entity.ToTable("id_block");

        //    entity.HasIndex(e => new { e.Name, e.SystemId }, "idx_id_block_name_system").IsUnique();

        //    entity.Property(e => e.Id)
        //        .HasDefaultValueSql("gen_random_uuid()")
        //        .HasColumnName("id");
        //    entity.Property(e => e.Name)
        //        .HasMaxLength(200)
        //        .HasColumnName("name");
        //    entity.Property(e => e.SystemId).HasColumnName("system_id");
        //});

        //modelBuilder.Entity<Reboot>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("reboot_pkey");

        //    entity.ToTable("reboot");

        //    entity.HasIndex(e => new { e.Name, e.SystemId }, "idx_reboot_name_system").IsUnique();

        //    entity.Property(e => e.Id)
        //        .HasDefaultValueSql("gen_random_uuid()")
        //        .ValueGeneratedOnAdd()
        //        .HasColumnName("id");
        //    entity.Property(e => e.Name)
        //        .HasMaxLength(200)
        //        .HasColumnName("name");
        //    entity.Property(e => e.SystemId).HasColumnName("system_id");
        //});

        //modelBuilder.Entity<RebootCommand>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("reboot_command_pkey");

        //    entity.ToTable("reboot_command");

        //    entity.HasIndex(e => new { e.RebootId, e.ExecutionOrder }, "idx_reboot_command_order").IsUnique();

        //    entity.HasIndex(e => e.RebootId, "idx_reboot_command_reboot_id");

        //    entity.Property(e => e.Id)
        //        .HasDefaultValueSql("gen_random_uuid()")
        //        .ValueGeneratedOnAdd()
        //        .HasColumnName("id");
        //    entity.Property(e => e.CommandText).HasColumnName("command_text");
        //    entity.Property(e => e.ExecutionOrder).HasColumnName("execution_order");
        //    entity.Property(e => e.RebootId).HasColumnName("reboot_id");

        //    entity.HasOne(d => d.Reboot).WithMany(p => p.RebootCommands)
        //        .HasForeignKey(d => d.RebootId)
        //        .HasConstraintName("fk_reboot");
        //});

        //modelBuilder.Entity<RebootEntry>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("reboot_entry_pkey");

        //    entity.ToTable("reboot_entry");

        //    entity.HasIndex(e => e.RebootId, "idx_reboot_entry_reboot_id");

        //    entity.HasIndex(e => e.ServerBlockId, "idx_reboot_entry_server_block_id");

        //    entity.HasIndex(e => e.ServerId, "idx_reboot_entry_server_id");

        //    entity.Property(e => e.Id)
        //        .HasDefaultValueSql("gen_random_uuid()")
        //        .ValueGeneratedOnAdd()
        //        .HasColumnName("id");
        //    entity.Property(e => e.RebootId).HasColumnName("reboot_id");
        //    entity.Property(e => e.ServerBlockId).HasColumnName("server_block_id");
        //    entity.Property(e => e.ServerId).HasColumnName("server_id");

        //    entity.HasOne(d => d.Reboot).WithMany(p => p.RebootEntries)
        //        .HasForeignKey(d => d.RebootId)
        //        .HasConstraintName("fk_reboot_entry");

        //    entity.HasOne(d => d.ServerBlock).WithMany(p => p.RebootEntries)
        //        .HasForeignKey(d => d.ServerBlockId)
        //        .HasConstraintName("fk_reboot_server_block");

        //    entity.HasOne(d => d.Server).WithMany(p => p.RebootEntries)
        //        .HasForeignKey(d => d.ServerId)
        //        .HasConstraintName("fk_reboot_server");
        //});

        //modelBuilder.Entity<SystemId>(entity =>
        //{
        //    entity.HasKey(e => e.Id).HasName("system_id_pkey");

        //    entity.ToTable("system_id");

        //    entity.HasIndex(e => e.BlockId, "idx_system_id_block_id");

        //    entity.HasIndex(e => new { e.BlockId, e.Name }, "idx_system_id_block_name").IsUnique();

        //    entity.Property(e => e.Id)
        //        .HasDefaultValueSql("gen_random_uuid()")
        //        .ValueGeneratedOnAdd()
        //        .HasColumnName("id");
        //    entity.Property(e => e.BlockId).HasColumnName("block_id");
        //    entity.Property(e => e.Content).HasColumnName("content");
        //    entity.Property(e => e.Name)
        //        .HasMaxLength(200)
        //        .HasColumnName("name");
        //});
    }
}