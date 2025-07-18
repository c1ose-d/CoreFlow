namespace CoreFlow.Presentation.Resources.Styles.Shared;

public static class ExpanderProperties
{
    private static readonly DependencyProperty WrapPanelDataTemplateProperty = DependencyProperty.RegisterAttached("WrapPanelDataTemplate", typeof(DataTemplate), typeof(ExpanderProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

    public static void SetWrapPanelDataTemplate(DependencyObject dependencyObject, DataTemplate dataTemplate)
    {
        dependencyObject.SetValue(WrapPanelDataTemplateProperty, dataTemplate);
    }

    public static DataTemplate GetWrapPanelDataTemplate(DependencyObject dependencyObject)
    {
        return (DataTemplate)dependencyObject.GetValue(WrapPanelDataTemplateProperty);
    }
}