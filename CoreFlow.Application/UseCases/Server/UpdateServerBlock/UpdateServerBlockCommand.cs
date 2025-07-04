namespace CoreFlow.Application.UseCases.Server.UpdateServerBlock;

public record UpdateServerBlockCommand(Guid Id, string NewName);