namespace CoreFlow.Presentation.Resources.Styles.Shared;

public static class ListItemProperties
{
    public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached("Icon", typeof(string), typeof(ListItemProperties), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static void SetIcon(DependencyObject dependencyObject, string value)
    {
        dependencyObject.SetValue(IconProperty, value);
    }

    public static string GetIcon(DependencyObject dependencyObject)
    {
        return (string)dependencyObject.GetValue(IconProperty);
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached("Text", typeof(string), typeof(ListItemProperties), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static void SetText(DependencyObject dependencyObject, string value)
    {
        dependencyObject.SetValue(TextProperty, value);
    }

    public static string GetText(DependencyObject dependencyObject)
    {
        return (string)dependencyObject.GetValue(TextProperty);
    }

    public static readonly DependencyProperty CaptionProperty = DependencyProperty.RegisterAttached("Caption", typeof(string), typeof(ListItemProperties), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static void SetCaption(DependencyObject dependencyObject, string value)
    {
        dependencyObject.SetValue(CaptionProperty, value);
    }

    public static string GetCaption(DependencyObject dependencyObject)
    {
        return (string)dependencyObject.GetValue(CaptionProperty);
    }
}