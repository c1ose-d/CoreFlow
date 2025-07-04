namespace CoreFlow.Persistence.Contexts;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CoreFlowDbContext>
{
    public CoreFlowDbContext CreateDbContext(string[] args)
    {
        string basePath = AppContext.BaseDirectory;
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();
        string connStr = config.GetConnectionString("CoreFlow")
            ?? throw new InvalidOperationException();
        DbContextOptionsBuilder<CoreFlowDbContext> optionsBuilder = new();
        _ = optionsBuilder.UseNpgsql(connStr);
        return new CoreFlowDbContext(optionsBuilder.Options);
    }
}