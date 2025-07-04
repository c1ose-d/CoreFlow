namespace CoreFlow.Presentation;

public partial class App : System.Windows.Application
{
    public static IHost AppHost { get; private set; } = null!;

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(config =>
            {
                _ = config.SetBasePath(AppContext.BaseDirectory);
                _ = config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                string? connectionString = context.Configuration.GetConnectionString("CoreFlow");

                _ = services.AddDbContext<CoreFlowDbContext>(options =>
                    options.UseNpgsql(connectionString));

                services.AddInfrastructure();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost.StartAsync();
        MainWindow mainWindow = new();
        mainWindow.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost.StopAsync();
        AppHost.Dispose();
        base.OnExit(e);
    }
}