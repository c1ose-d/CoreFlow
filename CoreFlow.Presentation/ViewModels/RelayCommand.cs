namespace CoreFlow.Presentation.ViewModels;

public sealed class RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null) : ICommand
{
    public RelayCommand(Action execute) : this(_ => execute()) { }

    private readonly Action<object?> _exec = execute ?? throw new ArgumentNullException(nameof(execute));
    private readonly Func<object?, bool>? _can = canExecute;

    public bool CanExecute(object? p)
    {
        return _can?.Invoke(p) ?? true;
    }

    public void Execute(object? p)
    {
        _exec(p);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value; remove => CommandManager.RequerySuggested -= value;
    }
}