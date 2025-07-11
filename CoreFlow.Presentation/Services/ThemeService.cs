namespace CoreFlow.Presentation.Services
{
    public class ThemeService(IOptions<ThemeOptions> options) : IThemeService
    {
        private readonly ThemeOptions _themeOptions = options.Value;
        private readonly string _configFilePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

        public void ApplyTheme()
        {
            Uri uri = new(_themeOptions.ThemePath, UriKind.Relative);
            ResourceDictionary resourceDictionary = new() { Source = uri };

            Collection<ResourceDictionary> merged = System.Windows.Application.Current.Resources.MergedDictionaries;
            int idx = merged.Select((d, i) => (d, i)).FirstOrDefault(x => x.d.Source?.OriginalString.Contains("/Themes/") == true).i;
            if (idx >= 0)
            {
                merged[idx] = resourceDictionary;
            }
            else
            {
                merged.Add(resourceDictionary);
            }
        }

        public void ToggleTheme()
        {
            bool isLight = _themeOptions.ThemePath.Contains("LightTheme.xaml", StringComparison.OrdinalIgnoreCase);
            _themeOptions.ThemePath = isLight ? "/CoreFlow.Presentation;component/Resources/Styles/Themes/DarkTheme.xaml" : "/CoreFlow.Presentation;component/Resources/Styles/Themes/LightTheme.xaml";

            try
            {
                string json = File.ReadAllText(_configFilePath);
                dynamic jObj = JObject.Parse(json);
                jObj["ThemePath"] = _themeOptions.ThemePath;
                File.WriteAllText(_configFilePath, jObj.ToString());
            }
            catch { }

            ApplyTheme();
        }
    }
}