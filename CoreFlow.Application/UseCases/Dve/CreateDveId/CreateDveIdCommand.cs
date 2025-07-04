namespace CoreFlow.Application.UseCases.Dve.CreateDveId;

public record CreateDveIdCommand(Guid BlockId, string Name, string Content);