namespace CoreFlow.Presentation.Services;

public interface ICurrentAppSystemService
{
    event EventHandler CurrentAppSystemChanged;

    void SetCurrentAppSystem(AppSystemDto appSystemDto);
    AppSystemDto? GetCurrentAppSystem();
}