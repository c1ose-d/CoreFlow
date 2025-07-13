namespace CoreFlow.Presentation.Services;

public interface IConfirmationDialogService
{
    Task<bool> Confirm(string body, string title);
}