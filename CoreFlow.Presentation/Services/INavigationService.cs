namespace CoreFlow.Presentation.Services;

public interface INavigationService
{
    bool CanGoBack { get; }

    void Configure(string key, Type pageType, bool cacheable = true);
    void NavigateTo(string pageKey, object? parameter = null);
    void GoBack();
}