namespace CoreFlow.Application.UseCases.Server.DeleteServerBlock;

public class DeleteServerBlockHandler(IServerBlockRepository repository)
{
    private readonly IServerBlockRepository _repository = repository;

    public async Task<DeleteServerBlockResult> HandleAsync(
        DeleteServerBlockCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        bool success = await _repository.RemoveAsync(command.Id, cancellationToken);
        return new DeleteServerBlockResult(success);
    }
}