namespace CoreFlow.Application.UseCases.Server.CreateServer;

public class CreateServerHandler(IServerRepository repository)
{
    private readonly IServerRepository _repository = repository;

    public async Task<CreateServerResult> HandleAsync(CreateServerCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.IpAddress))
        {
            throw new ArgumentException("IP address cannot be empty", nameof(command));
        }

        Domain.Server.Server server = new(Guid.NewGuid(), command.IpAddress, command.HostName, command.BlockId);

        await _repository.AddAsync(server, cancellationToken);

        return new CreateServerResult(server.Id, server.BlockId, server.IpAddress, server.HostName);
    }
}