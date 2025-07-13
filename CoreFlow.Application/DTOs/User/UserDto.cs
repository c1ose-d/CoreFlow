using CoreFlow.Application.DTOs.AppSystem;

namespace CoreFlow.Application.DTOs.User;

public record UserDto(Guid Id, string LastName, string FirstName, string? MiddleName, string UserName, bool IsAdmin, IReadOnlyCollection<AppSystemDto> AppSystems)
{
    public string FullName => string.IsNullOrWhiteSpace(MiddleName) ? $"{LastName} {FirstName}" : $"{LastName} {FirstName} {MiddleName}";
}