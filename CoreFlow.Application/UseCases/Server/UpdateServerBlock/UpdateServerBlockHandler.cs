namespace CoreFlow.Application.UseCases.Server.UpdateServerBlock;

public class UpdateServerBlockHandler(IServerBlockRepository repository)
{
    private readonly IServerBlockRepository _repository = repository;

    public async Task<UpdateServerBlockResult> HandleAsync(
        UpdateServerBlockCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.NewName))
        {
            throw new ArgumentException("NewName cannot be empty", nameof(command));
        }

        ServerBlock updated = new(command.Id, command.NewName);
        await _repository.UpdateAsync(updated, cancellationToken);

        return new UpdateServerBlockResult(updated.Id, updated.Name);
    }
}