namespace CoreFlow.Presentation.Services;

public interface ICurrentUserService
{
    void SetCurrentUser(UserDto userDto);
    UserDto? GetCurrentUser();
}