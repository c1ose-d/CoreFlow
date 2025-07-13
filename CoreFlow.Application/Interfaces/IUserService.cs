namespace CoreFlow.Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(Guid id);
    Task<UserDto> GetByUserNamePasswordAsync(string userName, string password);
    Task<IReadOnlyCollection<UserDto>> GetAllAsync();

    Task<IReadOnlyCollection<UserDto>> SearchAsync(string searchString);

    Task<UserDto> CreateAsync(CreateUserDto dto);

    Task<UserDto> UpdateAsync(UpdateUserDto dto);

    Task DeleteAsync(Guid id);
}