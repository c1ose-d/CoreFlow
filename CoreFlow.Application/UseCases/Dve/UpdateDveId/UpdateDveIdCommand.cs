namespace CoreFlow.Application.UseCases.Dve.UpdateDveId;

public record UpdateDveIdCommand(Guid Id, string NewName, string NewContent);