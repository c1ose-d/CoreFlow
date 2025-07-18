namespace CoreFlow.Application.Interfaces;

public interface IMonitorNetworkService
{
    IObservable<IEnumerable<ServerBlockResultDto>> StartMonitoring(IEnumerable<ServerBlockDto> targets, CancellationToken cancellationToken = default);
}