namespace CoreFlow.Application.UseCases.Dve.CreateDveBlock;

public class CreateDveBlockHandler(IDveBlockRepository repository)
{
    private readonly IDveBlockRepository _repository = repository;

    public async Task<CreateDveBlockResult> HandleAsync(CreateDveBlockCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(command));
        }

        bool exists = await _repository.ExistsAsync(command.Name, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException($"Dve block '{command.Name}' already exists");
        }

        DveBlock block = new(Guid.NewGuid(), command.Name);
        await _repository.AddAsync(block, cancellationToken);

        return new CreateDveBlockResult(block.Id, block.Name);
    }
}