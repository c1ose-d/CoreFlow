namespace CoreFlow.Application.UseCases.Server.CreateServerBlock;

public class CreateServerBlockHandler(IServerBlockRepository repository)
{
    private readonly IServerBlockRepository _repository = repository;

    public async Task<CreateServerBlockResult> HandleAsync(
        CreateServerBlockCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(command));
        }

        bool exists = await _repository.ExistsAsync(command.Name, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException($"Server block '{command.Name}' already exists");
        }

        ServerBlock block = new(Guid.NewGuid(), command.Name);
        await _repository.AddAsync(block, cancellationToken);

        return new CreateServerBlockResult(block.Id, block.Name);
    }
}