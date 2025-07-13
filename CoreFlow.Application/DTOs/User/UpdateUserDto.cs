namespace CoreFlow.Application.DTOs.User;

public record UpdateUserDto(Guid Id, string? LastName, string? FirstName, string? MiddleName, string? UserName, string? Password, bool? IsAdmin, IReadOnlyCollection<Guid>? AppSystemIds);