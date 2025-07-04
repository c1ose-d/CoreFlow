namespace CoreFlow.Application.UseCases.Server.DeleteServer;

public class DeleteServerHandler(IServerRepository repository)
{
    private readonly IServerRepository _repository = repository;

    public async Task<DeleteServerResult> HandleAsync(
        DeleteServerCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        bool success = await _repository.RemoveAsync(command.Id, cancellationToken);
        return new DeleteServerResult(success);
    }
}