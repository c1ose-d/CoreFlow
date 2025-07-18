namespace CoreFlow.Infrastructure.Data;

public partial class CoreFlowContext(DbContextOptions<CoreFlowContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<AppSystem> AppSystems => Set<AppSystem>();
    public DbSet<ServerBlock> ServerBlocks => Set<ServerBlock>();
    public DbSet<Server> Servers => Set<Server>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreFlowContext).Assembly);
    }
}