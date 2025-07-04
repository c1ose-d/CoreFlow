namespace CoreFlow.Application.UseCases.Linux.GetLinuxBlocks;

public class GetLinuxBlocksHandler(ILinuxBlockRepository repository)
{
    private readonly ILinuxBlockRepository _repository = repository;

    public Task<IReadOnlyList<LinuxBlock>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(cancellationToken);
    }
}