namespace CoreFlow.Application.UseCases.Server.GetServers;

public class GetServersHandler(IServerRepository repository)
{
    private readonly IServerRepository _repository = repository;

    public Task<IReadOnlyList<Domain.Server.Server>> HandleAsync(GetServersQuery query, CancellationToken cancellationToken = default)
    {
        return _repository.GetByBlockIdAsync(query.BlockId, cancellationToken);
    }
}