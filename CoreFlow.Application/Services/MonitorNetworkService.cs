namespace CoreFlow.Application.Services;

public class MonitorNetworkService(IPingService pingService) : IMonitorNetworkService
{
    private readonly IPingService _pingService = pingService;

    public IObservable<IEnumerable<ServerBlockResultDto>> StartMonitoring(IEnumerable<ServerBlockDto> serverBlocks, CancellationToken cancellationToken)
    {
        return Observable
            .Interval(TimeSpan.FromSeconds(5))
            .StartWith(0L)
            .TakeWhile(_ => !cancellationToken.IsCancellationRequested)
            .SelectMany(async _ =>
            {
                List<ServerBlockResultDto> serverBlockResults = [];

                foreach (ServerBlockDto serverBlock in serverBlocks)
                {
                    var pingTasks = serverBlock.Servers.Select(selector => _pingService.PingAsync(selector.IpAddress).ContinueWith(continuationAction => new { Dto = selector, Status = continuationAction.Result }, TaskScheduler.Default)).ToArray();

                    var results = await Task.WhenAll(pingTasks);

                    IEnumerable<ServerResultDto> srvResults = results.Select(r => new ServerResultDto(r.Dto.IpAddress, r.Dto.HostName ?? null, r.Status));

                    serverBlockResults.Add(new ServerBlockResultDto(serverBlock.Name, srvResults));
                }

                return (IEnumerable<ServerBlockResultDto>)serverBlockResults;
            });
    }
}