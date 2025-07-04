namespace CoreFlow.Application.UseCases.Server.GetServerBlocks;

public class GetServerBlocksHandler(IServerBlockRepository repository)
{
    private readonly IServerBlockRepository _repository = repository;

    public Task<IReadOnlyList<ServerBlock>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(cancellationToken);
    }
}