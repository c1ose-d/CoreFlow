namespace CoreFlow.Presentation.Services;

public class CurrentUserService : ICurrentUserService
{
    private UserDto? _currentUser;

    public void SetCurrentUser(UserDto userDto)
    {
        _currentUser = userDto;
    }

    public UserDto? GetCurrentUser()
    {
        if (_currentUser != null)
        {
            return _currentUser;
        }
        else
        {
            return null;
        }
    }
}