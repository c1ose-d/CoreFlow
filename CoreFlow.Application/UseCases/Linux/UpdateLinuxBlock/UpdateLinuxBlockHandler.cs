namespace CoreFlow.Application.UseCases.Linux.UpdateLinuxBlock;

public class UpdateLinuxBlockHandler(ILinuxBlockRepository repository)
{
    private readonly ILinuxBlockRepository _repository = repository;

    public async Task<UpdateLinuxBlockResult> HandleAsync(
        UpdateLinuxBlockCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.NewName))
        {
            throw new ArgumentException("NewName cannot be empty", nameof(command));
        }

        LinuxBlock updated = new(command.Id, command.NewName);
        await _repository.UpdateAsync(updated, cancellationToken);

        return new UpdateLinuxBlockResult(updated.Id, updated.Name);
    }
}