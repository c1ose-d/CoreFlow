namespace CoreFlow.Application.UseCases.Linux.UpdateLinuxCommand;

public class UpdateLinuxCommandHandler(ILinuxCommandRepository repository)
{
    private readonly ILinuxCommandRepository _repository = repository;

    public async Task<UpdateLinuxCommandResult> HandleAsync(
        UpdateLinuxCommandCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.NewName))
        {
            throw new ArgumentException("NewName cannot be empty", nameof(command));
        }

        if (string.IsNullOrWhiteSpace(command.NewContent))
        {
            throw new ArgumentException("NewContent cannot be empty", nameof(command));
        }

        LinuxCommand updated = new(command.Id, command.NewName, command.NewContent, Guid.Empty);
        await _repository.UpdateAsync(updated, cancellationToken);

        return new UpdateLinuxCommandResult(updated.Id, updated.Name, updated.Content);
    }
}