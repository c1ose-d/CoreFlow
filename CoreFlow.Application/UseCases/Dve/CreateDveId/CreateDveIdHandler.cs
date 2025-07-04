namespace CoreFlow.Application.UseCases.Dve.CreateDveId;

public class CreateDveIdHandler(IDveIdRepository repository)
{
    private readonly IDveIdRepository _repository = repository;

    public async Task<CreateDveIdResult> HandleAsync(CreateDveIdCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(command));
        }

        if (string.IsNullOrWhiteSpace(command.Content))
        {
            throw new ArgumentException("Content cannot be empty", nameof(command));
        }

        DveId dveId = new(Guid.NewGuid(), command.Name, command.Content, command.BlockId);
        await _repository.AddAsync(dveId, cancellationToken);

        return new CreateDveIdResult(dveId.Id, dveId.BlockId, dveId.Name, dveId.Content);
    }
}