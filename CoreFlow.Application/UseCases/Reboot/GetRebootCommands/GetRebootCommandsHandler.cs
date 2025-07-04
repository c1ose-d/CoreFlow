namespace CoreFlow.Application.UseCases.Reboot.GetRebootCommands;

public class GetRebootCommandsHandler(IRebootCommandRepository repository)
{
    private readonly IRebootCommandRepository _repository = repository;

    public Task<IReadOnlyList<RebootCommand>> HandleAsync(GetRebootCommandsQuery query, CancellationToken cancellationToken = default)
    {
        return _repository.GetByRebootIdAsync(query.RebootId, cancellationToken);
    }
}