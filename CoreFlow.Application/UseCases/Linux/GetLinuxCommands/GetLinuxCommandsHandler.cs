namespace CoreFlow.Application.UseCases.Linux.GetLinuxCommands;

public class GetLinuxCommandsHandler(ILinuxCommandRepository repository)
{
    private readonly ILinuxCommandRepository _repository = repository;

    public Task<IReadOnlyList<LinuxCommand>> HandleAsync(
        GetLinuxCommandsQuery query,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetByBlockIdAsync(query.BlockId, cancellationToken);
    }
}