namespace CoreFlow.Application.UseCases.Dve.UpdateDveBlock;

public class UpdateDveBlockHandler(IDveBlockRepository repository)
{
    private readonly IDveBlockRepository _repository = repository;

    public async Task<UpdateDveBlockResult> HandleAsync(
        UpdateDveBlockCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.NewName))
        {
            throw new ArgumentException("NewName cannot be empty", nameof(command));
        }

        DveBlock updated = new(command.Id, command.NewName);
        await _repository.UpdateAsync(updated, cancellationToken);

        return new UpdateDveBlockResult(updated.Id, updated.Name);
    }
}