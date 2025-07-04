namespace CoreFlow.Application.UseCases.Reboot.CreateReboot;

public class CreateRebootHandler(IRebootRepository repository)
{
    private readonly IRebootRepository _repository = repository;

    public async Task<CreateRebootResult> HandleAsync(CreateRebootCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(command));
        }

        bool exists = await _repository.ExistsAsync(command.Name, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException($"Reboot '{command.Name}' already exists");
        }

        Domain.Reboot.Reboot reboot = new(Guid.NewGuid(), command.Name);
        await _repository.AddAsync(reboot, cancellationToken);

        return new CreateRebootResult(reboot.Id, reboot.Name);
    }
}