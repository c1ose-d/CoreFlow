namespace CoreFlow.Presentation.Resources.Styles.Shared;

public static class TreeViewProperties
{
    static TreeViewProperties()
    {
        EventManager.RegisterClassHandler(typeof(TreeView), TreeView.SelectedItemChangedEvent, new RoutedPropertyChangedEventHandler<object>(OnTreeViewSelectedItemChanged));
    }

    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.RegisterAttached("SelectedItem", typeof(object), typeof(TreeViewProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedItemPropertyChanged));

    public static object? GetSelectedItem(DependencyObject obj)
    {
        return obj.GetValue(SelectedItemProperty);
    }

    public static void SetSelectedItem(DependencyObject obj, object? value)
    {
        obj.SetValue(SelectedItemProperty, value);
    }

    private static void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        SetSelectedItem((DependencyObject)sender, e.NewValue);
    }

    private static void OnSelectedItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TreeView tree)
        {
            return;
        }

        if (e.NewValue is not null && TrySelectContainer(tree, e.NewValue)) { }
    }

    private static bool TrySelectContainer(ItemsControl parent, object itemToSelect)
    {
        if (parent.ItemContainerGenerator.ContainerFromItem(itemToSelect) is TreeViewItem tvi)
        {
            tvi.IsSelected = true;
            tvi.BringIntoView();
            return true;
        }

        foreach (object? childItem in parent.Items)
        {
            if (parent.ItemContainerGenerator.ContainerFromItem(childItem) is not TreeViewItem childContainer)
            {
                continue;
            }

            if (!childContainer.IsExpanded)
            {
                childContainer.IsExpanded = true;
                childContainer.UpdateLayout();
            }

            if (TrySelectContainer(childContainer, itemToSelect))
            {
                return true;
            }
        }

        return false;
    }
}