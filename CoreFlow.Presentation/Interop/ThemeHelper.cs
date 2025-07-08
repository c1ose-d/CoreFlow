namespace CoreFlow.Presentation.Interop;

internal static class ThemeHelper
{
    private const string KeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
    private const string ValueName = "SystemUsesLightTheme";

    internal static bool IsSystemLight()
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey(KeyPath);
        return (key?.GetValue(ValueName) as int? ?? 1) != 0;
    }
}