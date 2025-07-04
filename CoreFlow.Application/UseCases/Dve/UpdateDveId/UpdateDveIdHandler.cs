namespace CoreFlow.Application.UseCases.Dve.UpdateDveId;

public class UpdateDveIdHandler(IDveIdRepository repository)
{
    private readonly IDveIdRepository _repository = repository;

    public async Task<UpdateDveIdResult> HandleAsync(
        UpdateDveIdCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.NewName))
        {
            throw new ArgumentException("NewName cannot be empty", nameof(command));
        }

        if (string.IsNullOrWhiteSpace(command.NewContent))
        {
            throw new ArgumentException("NewContent cannot be empty", nameof(command));
        }

        DveId updated = new(command.Id, command.NewName, command.NewContent, Guid.Empty);
        await _repository.UpdateAsync(updated, cancellationToken);

        return new UpdateDveIdResult(updated.Id, updated.Name, updated.Content);
    }
}