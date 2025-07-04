namespace CoreFlow.Application.UseCases.Linux.DeleteLinuxBlock;

public class DeleteLinuxBlockHandler(ILinuxBlockRepository repository)
{
    private readonly ILinuxBlockRepository _repository = repository;

    public async Task<DeleteLinuxBlockResult> HandleAsync(
        DeleteLinuxBlockCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        bool success = await _repository.RemoveAsync(command.Id, cancellationToken);
        return new DeleteLinuxBlockResult(success);
    }
}