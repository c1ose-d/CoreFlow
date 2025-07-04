namespace CoreFlow.Application.UseCases.Linux.DeleteLinuxCommand;

public class DeleteLinuxCommandHandler(ILinuxCommandRepository repository)
{
    private readonly ILinuxCommandRepository _repository = repository;

    public async Task<DeleteLinuxCommandResult> HandleAsync(
        DeleteLinuxCommandCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        bool success = await _repository.RemoveAsync(command.Id, cancellationToken);
        return new DeleteLinuxCommandResult(success);
    }
}