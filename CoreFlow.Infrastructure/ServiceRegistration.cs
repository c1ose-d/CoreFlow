namespace CoreFlow.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        _ = services.AddScoped<IUnitOfWork, UnitOfWork>();
        _ = services.AddScoped<IDveBlockRepository, DveBlockRepository>();
        _ = services.AddScoped<ILinuxBlockRepository, LinuxBlockRepository>();
        _ = services.AddScoped<IServerBlockRepository, ServerBlockRepository>();
        _ = services.AddScoped<IRebootRepository, RebootRepository>();
        _ = services.AddScoped<IRebootCommandRepository, RebootCommandRepository>();
        _ = services.AddScoped<IRebootListRepository, RebootListEntryRepository>();
        _ = services.AddScoped<IDveIdRepository, DveIdRepository>();
        _ = services.AddScoped<ILinuxCommandRepository, LinuxCommandRepository>();
        _ = services.AddScoped<IServerRepository, ServerRepository>();
    }
}