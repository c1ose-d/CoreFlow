namespace CoreFlow.Application.UseCases.Linux.CreateLinuxCommand;

public class CreateLinuxCommandHandler(ILinuxCommandRepository repository)
{
    private readonly ILinuxCommandRepository _repository = repository;

    public async Task<CreateLinuxCommandResult> HandleAsync(CreateLinuxCommandCommand command, CancellationToken cancellationToken = default)
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

        LinuxCommand linuxCommand = new(Guid.NewGuid(), command.Name, command.Content, command.BlockId);
        await _repository.AddAsync(linuxCommand, cancellationToken);

        return new CreateLinuxCommandResult(linuxCommand.Id, linuxCommand.BlockId, linuxCommand.Name, linuxCommand.Content);
    }
}