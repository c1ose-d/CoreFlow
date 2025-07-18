namespace CoreFlow.Presentation.Services;

public class CurrentUserService : ICurrentUserService
{
    private UserDto? _currentUser;

    public event EventHandler? CurrentUserChanged;

    public void SetCurrentUser(UserDto userDto)
    {
        _currentUser = userDto;
        CurrentUserChanged?.Invoke(this, EventArgs.Empty);
    }

    public UserDto? GetCurrentUser()
    {
        return _currentUser;
    }
}