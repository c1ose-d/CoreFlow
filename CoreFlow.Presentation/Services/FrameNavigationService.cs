namespace CoreFlow.Presentation.Services;

public class FrameNavigationService(IServiceProvider serviceProvider) : INavigationService
{
    private Frame? _frame;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly Dictionary<string, Type> _pagesByKey = [];
    private readonly Dictionary<string, bool> _cacheFlags = [];
    private readonly Dictionary<string, Page> _pageCache = [];

    public bool CanGoBack => _frame!.CanGoBack;

    public void Initialize(Frame frame)
    {
        _frame = frame;
    }

    public void Configure(string key, Type pageType, bool cacheable = true)
    {
        _pagesByKey[key] = pageType;
        _cacheFlags[key] = cacheable;
    }

    public void NavigateTo(string pageKey, object? parameter = null)
    {
        if (!_pagesByKey.TryGetValue(pageKey, out Type? value))
        {
            throw new InvalidOperationException($"Страница '{pageKey}' не зарегистрирована.");
        }

        bool cacheable = _cacheFlags[pageKey];

        if (cacheable && _pageCache.TryGetValue(pageKey, out Page? cached))
        {
            _ = _frame!.Navigate(cached);
            return;
        }

        Page page = (Page)_serviceProvider.GetRequiredService(value);

        if (cacheable)
        {
            _pageCache[pageKey] = page;
        }

        if (parameter != null && page.DataContext is INavigationAware vm)
        {
            vm.OnNavigatedTo(parameter);
        }

        _ = _frame!.Navigate(page);
    }

    public void GoBack()
    {
        if (_frame!.CanGoBack)
        {
            _frame.GoBack();
        }
    }
}