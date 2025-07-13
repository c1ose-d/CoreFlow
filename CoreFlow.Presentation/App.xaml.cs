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
            using IServiceScope serviceScope = host.Services.CreateScope();
            CoreFlowContext coreFlowContext = serviceScope.ServiceProvider.GetRequiredService<CoreFlowContext>();
            try
            {
                await coreFlowContext.Database.MigrateAsync();
                await coreFlowContext.Database.OpenConnectionAsync();
                await coreFlowContext.Database.CloseConnectionAsync();
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

            _ = serviceCollection.AddDbContext<CoreFlowContext>(optionsAction => optionsAction.UseNpgsql(hostBuilderContext.Configuration.GetConnectionString("CoreFlow")).UseSnakeCaseNamingConvention());

            _ = serviceCollection.AddScoped<IUserRepository, UserRepository>();
            _ = serviceCollection.AddScoped<IAppSystemRepository, AppSystemRepository>();

            _ = serviceCollection.AddScoped<IUserService, UserService>();
            _ = serviceCollection.AddScoped<IAppSystemService, AppSystemService>();

            _ = serviceCollection.AddScoped<IMainWindowService, MainWindowService>();
            _ = serviceCollection.AddScoped<ILoginWindowService, LoginWindowService>();

            _ = serviceCollection.Configure<ThemeOptions>(hostBuilderContext.Configuration);
            _ = serviceCollection.AddSingleton<IThemeService, ThemeService>();

            _ = serviceCollection.AddSingleton<ICurrentUserService, CurrentUserService>();

            _ = serviceCollection.AddSingleton<INotificationService, NotificationService>();

            _ = serviceCollection.AddSingleton<IConfirmationDialogService, ConfirmationDialogService>();

            _ = serviceCollection.AddTransient<MainWindow>();
            _ = serviceCollection.AddTransient<MainWindowViewModel>();

            _ = serviceCollection.AddTransient<LoginWindow>();
            _ = serviceCollection.AddTransient<LoginWindowViewModel>();

            _ = serviceCollection.AddTransient<TitleBar>();
            _ = serviceCollection.AddTransient<TitleBarViewModel>();

            _ = serviceCollection.AddTransient<SideNav>();
            _ = serviceCollection.AddTransient<SideNavViewModel>();

            _ = serviceCollection.AddTransient<SettingsPage>();
            _ = serviceCollection.AddTransient<SettingsPageViewModel>();

            _ = serviceCollection.AddTransient<AppSystemsPage>();
            _ = serviceCollection.AddTransient<AppSystemsPageViewModel>();

            _ = serviceCollection.AddTransient<UsersPage>();
            _ = serviceCollection.AddTransient<UsersPageViewModel>();

            _ = serviceCollection.AddSingleton<FrameNavigationService>();
            _ = serviceCollection.AddSingleton<INavigationService>(implementationFactory => implementationFactory.GetRequiredService<FrameNavigationService>());
        });
    }

    private static void ShowWindow<TWindow>(IHost host) where TWindow : Window
    {
        IServiceScope serviceScope = host.Services.CreateScope();
        IServiceProvider serviceProvider = serviceScope.ServiceProvider;

        MainWindow mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        IMainWindowService windowService = serviceProvider.GetRequiredService<IMainWindowService>();
        IThemeService themeService = serviceProvider.GetRequiredService<IThemeService>();

        FrameNavigationService frameNavigationService = serviceProvider.GetRequiredService<FrameNavigationService>();
        frameNavigationService.Initialize(mainWindow.Frame);
        frameNavigationService.Configure("Settings", typeof(SettingsPage), cacheable: false);
        frameNavigationService.Configure("AppSystems", typeof(AppSystemsPage), cacheable: true);
        frameNavigationService.Configure("Users", typeof(UsersPage), cacheable: true);

        windowService.Initialize(mainWindow);
        themeService.ApplyTheme();

        mainWindow.Show();
        mainWindow.Closed += (_, _) => serviceScope.Dispose();
    }
}