namespace CoreFlow.Application.UseCases.Dve.GetDveBlocks;

public class GetDveBlocksHandler(IDveBlockRepository repository)
{
    private readonly IDveBlockRepository _repository = repository;

    public Task<IReadOnlyList<DveBlock>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(cancellationToken);
    }
}