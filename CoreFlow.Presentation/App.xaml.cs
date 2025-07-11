namespace CoreFlow.Presentation;

public partial class App : System.Windows.Application
{
    [STAThread]
    public static void Main(string[] args)
    {
        AppContext.SetSwitch("Switch.System.Windows.Controls.Text.UseAdornerForTextboxSelectionRendering", false);

        App app = new();
        app.InitializeComponent();

        using IHost host = CreateHostBuilder(args).Build();
        host.Start();

        _ = Task.Run(async () =>
        {
            using var scope = host.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<CoreFlowContext>();
            try
            {
                await db.Database.OpenConnectionAsync();
                await db.Database.CloseConnectionAsync();
            }
            catch { }
        });

        ShowWindow<MainWindow>(host);
        _ = app.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureServices((hostBuilderContext, serviceCollection) =>
        {
            _ = serviceCollection.AddAutoMapper(configAction => configAction.AddProfile<MappingProfile>());

            _ = serviceCollection.AddDbContext<CoreFlowContext>(dbContextOptionsBuilder => dbContextOptionsBuilder.UseNpgsql(hostBuilderContext.Configuration.GetConnectionString("CoreFlow")));

            _ = serviceCollection.AddScoped<IUserRepository, UserRepository>();
            _ = serviceCollection.AddScoped<ISystemRepository, SystemRepository>();

            _ = serviceCollection.AddScoped<IUserService, UserService>();
            _ = serviceCollection.AddScoped<ISystemService, SystemService>();

            _ = serviceCollection.AddScoped<IMainWindowService, MainWindowService>();
            _ = serviceCollection.AddScoped<ILoginWindowService, LoginWindowService>();

            _ = serviceCollection.Configure<ThemeOptions>(hostBuilderContext.Configuration);
            _ = serviceCollection.AddScoped<IThemeService, ThemeService>();

            _ = serviceCollection.AddSingleton<ICurrentUserService, CurrentUserService>();

            _ = serviceCollection.AddSingleton<INotificationService, NotificationService>();

            _ = serviceCollection.AddScoped<MainWindowViewModel>();
            _ = serviceCollection.AddScoped<LoginWindowViewModel>();

            _ = serviceCollection.AddScoped<TitleBarViewModel>();

            _ = serviceCollection.AddScoped<MainWindow>();
            _ = serviceCollection.AddScoped<LoginWindow>();

            _ = serviceCollection.AddScoped<TitleBar>();
        });
    }

    private static void ShowWindow<TWindow>(IHost host) where TWindow : Window
    {
        IServiceScope serviceScope = host.Services.CreateScope();
        IServiceProvider serviceProvider = serviceScope.ServiceProvider;

        TWindow window = serviceProvider.GetRequiredService<TWindow>();
        IMainWindowService windowService = serviceProvider.GetRequiredService<IMainWindowService>();
        IThemeService themeService = serviceProvider.GetRequiredService<IThemeService>();

        windowService.Initialize(window);
        themeService.ApplyTheme();

        window.Show();
        window.Closed += (_, _) => serviceScope.Dispose();
    }
}