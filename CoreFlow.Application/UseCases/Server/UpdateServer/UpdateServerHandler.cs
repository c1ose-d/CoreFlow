namespace CoreFlow.Application.UseCases.Server.UpdateServer;

public class UpdateServerHandler(IServerRepository repository)
{
    private readonly IServerRepository _repository = repository;

    public async Task<UpdateServerResult> HandleAsync(
        UpdateServerCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.NewIpAddress))
        {
            throw new ArgumentException("NewIpAddress cannot be empty", nameof(command));
        }

        var updated = new Domain.Server.Server(command.Id, command.NewIpAddress, command.NewHostName, Guid.Empty);
        await _repository.UpdateAsync(updated, cancellationToken);

        return new UpdateServerResult(updated.Id, updated.IpAddress, updated.HostName);
    }
}