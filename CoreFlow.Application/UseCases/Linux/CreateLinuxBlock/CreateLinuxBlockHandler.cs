namespace CoreFlow.Application.UseCases.Linux.CreateLinuxBlock;

public class CreateLinuxBlockHandler(ILinuxBlockRepository repository)
{
    private readonly ILinuxBlockRepository _repository = repository;

    public async Task<CreateLinuxBlockResult> HandleAsync(CreateLinuxBlockCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(command));
        }

        bool alreadyExists = await _repository.ExistsAsync(command.Name, cancellationToken);
        if (alreadyExists)
        {
            throw new InvalidOperationException($"Linux block '{command.Name}' already exists");
        }

        LinuxBlock block = new(Guid.NewGuid(), command.Name);
        await _repository.AddAsync(block, cancellationToken);

        return new CreateLinuxBlockResult(block.Id, block.Name);
    }
}