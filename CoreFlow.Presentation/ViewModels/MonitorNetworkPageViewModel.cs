namespace CoreFlow.Presentation.ViewModels;

public partial class MonitorNetworkPageViewModel(IServerBlockService serverBlockService, IMonitorNetworkService monitorNetworkService, ICurrentAppSystemService currentAppSystemService) : ObservableObject, IPageLoadedAware
{
    private readonly IServerBlockService _serverBlockService = serverBlockService;
    private readonly IMonitorNetworkService _monitorNetworkService = monitorNetworkService;
    private readonly ICurrentAppSystemService _currentAppSystemService = currentAppSystemService;

    private CancellationTokenSource? _cancellationTokenSource;
    private IDisposable? _disposable;

    [ObservableProperty]
    private ObservableCollection<ServerBlockResultDto> _serverBlockResultDtos = [];

    public async Task Loaded()
    {
        _currentAppSystemService.CurrentAppSystemChanged += CurrentAppSystemChanged;

        await StartAsync();
    }

    private async void CurrentAppSystemChanged(object? sender, EventArgs e)
    {
        await StartAsync();
    }

    private async Task StartAsync()
    {
        _cancellationTokenSource?.Cancel();
        _disposable?.Dispose();

        ServerBlockResultDtos.Clear();

        _cancellationTokenSource = new CancellationTokenSource();

        IReadOnlyCollection<ServerBlockDto> serverBlockDtos = await _serverBlockService.GetByAppSystemIdAsync(_currentAppSystemService.GetCurrentAppSystem()!.Id).ConfigureAwait(false);

        _disposable = _monitorNetworkService
            .StartMonitoring(serverBlockDtos, _cancellationTokenSource.Token)
            .TakeWhile(_ => !_cancellationTokenSource.Token.IsCancellationRequested)
            .Subscribe(onNext =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    UpdateBlocks(onNext);
                });
            });
    }

    private void UpdateBlocks(IEnumerable<ServerBlockResultDto> serverBlockResultDtos)
    {
        List<ServerBlockResultDto> toRemove = [.. ServerBlockResultDtos.Where(vm => serverBlockResultDtos.All(d => d.Name != vm.Name))];
        foreach (ServerBlockResultDto? old in toRemove)
        {
            _ = ServerBlockResultDtos.Remove(old);
        }

        foreach (ServerBlockResultDto dto in serverBlockResultDtos)
        {
            ServerBlockResultDto? existing = ServerBlockResultDtos.FirstOrDefault(b => b.Name == dto.Name);

            if (existing == null)
            {
                ServerBlockResultDtos.Add(dto);
            }
            else
            {
                ServerBlockResultDto updated = existing with { Servers = dto.Servers };
                int idx = ServerBlockResultDtos.IndexOf(existing);
                if (idx >= 0)
                {
                    ServerBlockResultDtos[idx] = updated;
                }
            }
        }

    }

    public void Dispose()
    {
        _currentAppSystemService.CurrentAppSystemChanged -= CurrentAppSystemChanged;
        _disposable?.Dispose();
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}