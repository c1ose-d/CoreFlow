namespace CoreFlow.Presentation.Resources.Styles.Shared;

public static class NavItemProperties
{
    public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached("Icon", typeof(string), typeof(NavItemProperties), new FrameworkPropertyMetadata(default(ImageSource), FrameworkPropertyMetadataOptions.AffectsRender));

    public static void SetIcon(DependencyObject dependencyObject, string value)
    {
        dependencyObject.SetValue(IconProperty, value);
    }

    public static string GetIcon(DependencyObject dependencyObject)
    {
        return (string)dependencyObject.GetValue(IconProperty);
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached("Text", typeof(string), typeof(NavItemProperties), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static void SetText(DependencyObject dependencyObject, string value)
    {
        dependencyObject.SetValue(TextProperty, value);
    }

    public static string GetText(DependencyObject dependencyObject)
    {
        return (string)dependencyObject.GetValue(TextProperty);
    }
}