namespace CoreFlow.Application.UseCases.Dve.GetDveIds;

public class GetDveIdsHandler(IDveIdRepository repository)
{
    private readonly IDveIdRepository _repository = repository;

    public Task<IReadOnlyList<DveId>> HandleAsync(GetDveIdsQuery query, CancellationToken cancellationToken = default)
    {
        return _repository.GetByBlockIdAsync(query.BlockId, cancellationToken);
    }
}