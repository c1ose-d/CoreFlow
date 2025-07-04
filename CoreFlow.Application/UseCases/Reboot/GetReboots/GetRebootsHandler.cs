namespace CoreFlow.Application.UseCases.Reboot.GetReboots;

public class GetRebootsHandler(IRebootRepository repository)
{
    private readonly IRebootRepository _repository = repository;

    public Task<IReadOnlyList<Domain.Reboot.Reboot>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(cancellationToken);
    }
}