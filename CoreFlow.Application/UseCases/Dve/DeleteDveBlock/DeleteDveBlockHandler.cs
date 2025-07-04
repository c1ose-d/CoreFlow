namespace CoreFlow.Application.UseCases.Dve.DeleteDveBlock;

public class DeleteDveBlockHandler(IDveBlockRepository repository)
{
    private readonly IDveBlockRepository _repository = repository;

    public async Task<DeleteDveBlockResult> HandleAsync(
        DeleteDveBlockCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        bool success = await _repository.RemoveAsync(command.Id, cancellationToken);
        return new DeleteDveBlockResult(success);
    }
}