namespace CoreFlow.Application.UseCases.Reboot.GetRebootList;

public class GetRebootListHandler(IRebootListRepository repository)
{
    private readonly IRebootListRepository _repository = repository;

    public Task<IReadOnlyList<RebootListEntry>> HandleAsync(GetRebootListQuery query, CancellationToken cancellationToken = default)
    {
        return _repository.GetByRebootIdAsync(query.RebootId, cancellationToken);
    }
}