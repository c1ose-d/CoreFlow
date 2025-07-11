namespace CoreFlow.Presentation.Services;

public interface ILoginWindowService
{
    Task<UserDto?> ShowDialogAsync(bool? onLoaded);
}