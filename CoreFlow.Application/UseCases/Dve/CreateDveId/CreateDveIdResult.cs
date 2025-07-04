namespace CoreFlow.Application.UseCases.Dve.CreateDveId;

public record CreateDveIdResult(Guid Id, Guid BlockId, string Name, string Content);