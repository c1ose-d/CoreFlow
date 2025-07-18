namespace CoreFlow.Presentation.Services;

public interface ICurrentUserService
{
    event EventHandler CurrentUserChanged;

    void SetCurrentUser(UserDto userDto);
    UserDto? GetCurrentUser();
}