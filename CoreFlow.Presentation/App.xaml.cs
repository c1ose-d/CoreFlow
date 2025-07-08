using CoreFlow.Presentation.Services;

namespace CoreFlow.Presentation;

public partial class App : System.Windows.Application
{
    public App()
    {
        System.Collections.ObjectModel.Collection<ResourceDictionary> dicts = Current.Resources.MergedDictionaries;
        dicts.Add(new ResourceDictionary
        {
            Source = new Uri("./Resources/Themes/Generic.xaml", UriKind.Relative)
        });

        MainWindow window = new();
        WindowService service = new(window);
        MainWindowViewModel vm = new(service);
        window.DataContext = vm;
        window.Show();
    }
}