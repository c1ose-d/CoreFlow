namespace CoreFlow.Application.UseCases.Dve.DeleteDveId;

public class DeleteDveIdHandler(IDveIdRepository repository)
{
    private readonly IDveIdRepository _repository = repository;

    public async Task<DeleteDveIdResult> HandleAsync(DeleteDveIdCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        bool success = await _repository.RemoveAsync(command.Id, cancellationToken);
        return new DeleteDveIdResult(success);
    }
}