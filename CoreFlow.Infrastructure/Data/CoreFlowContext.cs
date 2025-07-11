namespace CoreFlow.Infrastructure.Data;

public partial class CoreFlowContext(DbContextOptions<CoreFlowContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public virtual DbSet<Command> Commands => Set<Command>();

    public virtual DbSet<CommandBlock> CommandBlocks => Set<CommandBlock>();

    public virtual DbSet<IdBlock> IdBlocks => Set<IdBlock>();

    public virtual DbSet<Reboot> Reboots => Set<Reboot>();

    public virtual DbSet<RebootCommand> RebootCommands => Set<RebootCommand>();

    public virtual DbSet<RebootEntry> RebootEntries => Set<RebootEntry>();

    public virtual DbSet<Server> Servers => Set<Server>();

    public virtual DbSet<ServerBlock> ServerBlocks => Set<ServerBlock>();

    public virtual DbSet<Domain.Entities.System> Systems => Set<Domain.Entities.System>();

    public virtual DbSet<SystemId> SystemIds => Set<SystemId>();

    public virtual DbSet<User> Users => Set<User>();

    public virtual DbSet<UserSystem> UserSystems => Set<UserSystem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Command>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("command_pkey");

            entity.ToTable("command");

            entity.HasIndex(e => e.BlockId, "idx_command_block_id");

            entity.HasIndex(e => new { e.BlockId, e.Name }, "idx_command_block_name").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.BlockId).HasColumnName("block_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");

            entity.HasOne(d => d.Block).WithMany(p => p.Commands)
                .HasForeignKey(d => d.BlockId)
                .HasConstraintName("fk_command_block");
        });

        modelBuilder.Entity<CommandBlock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("command_block_pkey");

            entity.ToTable("command_block");

            entity.HasIndex(e => new { e.Name, e.SystemId }, "idx_command_block_name_system").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.SystemId).HasColumnName("system_id");
        });

        modelBuilder.Entity<IdBlock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("id_block_pkey");

            entity.ToTable("id_block");

            entity.HasIndex(e => new { e.Name, e.SystemId }, "idx_id_block_name_system").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.SystemId).HasColumnName("system_id");
        });

        modelBuilder.Entity<Reboot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reboot_pkey");

            entity.ToTable("reboot");

            entity.HasIndex(e => new { e.Name, e.SystemId }, "idx_reboot_name_system").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.SystemId).HasColumnName("system_id");
        });

        modelBuilder.Entity<RebootCommand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reboot_command_pkey");

            entity.ToTable("reboot_command");

            entity.HasIndex(e => new { e.RebootId, e.ExecutionOrder }, "idx_reboot_command_order").IsUnique();

            entity.HasIndex(e => e.RebootId, "idx_reboot_command_reboot_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.CommandText).HasColumnName("command_text");
            entity.Property(e => e.ExecutionOrder).HasColumnName("execution_order");
            entity.Property(e => e.RebootId).HasColumnName("reboot_id");

            entity.HasOne(d => d.Reboot).WithMany(p => p.RebootCommands)
                .HasForeignKey(d => d.RebootId)
                .HasConstraintName("fk_reboot");
        });

        modelBuilder.Entity<RebootEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reboot_entry_pkey");

            entity.ToTable("reboot_entry");

            entity.HasIndex(e => e.RebootId, "idx_reboot_entry_reboot_id");

            entity.HasIndex(e => e.ServerBlockId, "idx_reboot_entry_server_block_id");

            entity.HasIndex(e => e.ServerId, "idx_reboot_entry_server_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.RebootId).HasColumnName("reboot_id");
            entity.Property(e => e.ServerBlockId).HasColumnName("server_block_id");
            entity.Property(e => e.ServerId).HasColumnName("server_id");

            entity.HasOne(d => d.Reboot).WithMany(p => p.RebootEntries)
                .HasForeignKey(d => d.RebootId)
                .HasConstraintName("fk_reboot_entry");

            entity.HasOne(d => d.ServerBlock).WithMany(p => p.RebootEntries)
                .HasForeignKey(d => d.ServerBlockId)
                .HasConstraintName("fk_reboot_server_block");

            entity.HasOne(d => d.Server).WithMany(p => p.RebootEntries)
                .HasForeignKey(d => d.ServerId)
                .HasConstraintName("fk_reboot_server");
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("server_pkey");

            entity.ToTable("server");

            entity.HasIndex(e => e.BlockId, "idx_server_block_id");

            entity.HasIndex(e => new { e.IpAddress, e.BlockId }, "idx_server_ip_address_block").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.BlockId).HasColumnName("block_id");
            entity.Property(e => e.HostName)
                .HasMaxLength(200)
                .HasColumnName("host_name");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(100)
                .HasColumnName("ip_address");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Block).WithMany(p => p.Servers)
                .HasForeignKey(d => d.BlockId)
                .HasConstraintName("fk_server_block");
        });

        modelBuilder.Entity<ServerBlock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("server_block_pkey");

            entity.ToTable("server_block");

            entity.HasIndex(e => new { e.Name, e.SystemId }, "idx_server_block_name_system").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.SystemId).HasColumnName("system_id");
        });

        modelBuilder.Entity<Domain.Entities.System>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("system_pkey");

            entity.ToTable("system");

            entity.HasIndex(e => e.Name, "idx_system_name").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.ShortName)
                .HasMaxLength(50)
                .HasColumnName("short_name");
        });

        modelBuilder.Entity<SystemId>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("system_id_pkey");

            entity.ToTable("system_id");

            entity.HasIndex(e => e.BlockId, "idx_system_id_block_id");

            entity.HasIndex(e => new { e.BlockId, e.Name }, "idx_system_id_block_name").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.BlockId).HasColumnName("block_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.UserName, "idx_user_user_name").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.IsAdmin)
                .HasDefaultValue(false)
                .ValueGeneratedOnAdd()
                .HasColumnName("is_admin");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasColumnName("middle_name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<UserSystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_system_pkey");

            entity.ToTable("user_system");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.SystemId).HasColumnName("system_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.System).WithMany(p => p.UserSystems)
                .HasForeignKey(d => d.SystemId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_system");

            entity.HasOne(d => d.User).WithMany(p => p.UserSystems)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}