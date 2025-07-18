namespace CoreFlow.Presentation.Resources.Styles.Shared;

public static class ListViewProperties
{
    private static readonly Dictionary<ListView, NotifyCollectionChangedEventHandler> _colChangedHandlers = [];

    public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(ListViewProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedItemsChanged));

    public static void SetSelectedItems(DependencyObject element, IList value)
    {
        element.SetValue(SelectedItemsProperty, value);
    }

    public static IList GetSelectedItems(DependencyObject element)
    {
        return (IList)element.GetValue(SelectedItemsProperty);
    }

    private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ListView lv)
        {
            return;
        }

        lv.SelectionChanged -= ListView_SelectionChanged;
        lv.SelectionChanged += ListView_SelectionChanged;

        if (_colChangedHandlers.TryGetValue(lv, out NotifyCollectionChangedEventHandler? oldHandler) && e.OldValue is INotifyCollectionChanged oldCol)
        {
            oldCol.CollectionChanged -= oldHandler;
        }

        if (e.NewValue is INotifyCollectionChanged newCol)
        {
            void handler(object? s, NotifyCollectionChangedEventArgs args)
            {
                BoundCollectionChanged(lv, args);
            }

            _colChangedHandlers[lv] = handler;
            newCol.CollectionChanged += handler;
        }

        if (e.NewValue is IEnumerable list)
        {
            lv.SelectionChanged -= ListView_SelectionChanged;

            lv.SelectedItems.Clear();
            foreach (object? item in list)
            {
                _ = lv.SelectedItems.Add(item);
            }

            lv.SelectionChanged += ListView_SelectionChanged;
        }
    }

    private static void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ListView lv = (ListView)sender;
        if (GetSelectedItems(lv) is not IList bound)
        {
            return;
        }

        foreach (object? item in e.RemovedItems)
        {
            bound.Remove(item);
        }

        foreach (object? item in e.AddedItems)
        {
            _ = bound.Add(item);
        }
    }

    private static void BoundCollectionChanged(ListView lv, NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems != null)
        {
            foreach (object? item in e.OldItems)
            {
                lv.SelectedItems.Remove(item);
            }
        }

        if (e.NewItems != null)
        {
            foreach (object? item in e.NewItems)
            {
                _ = lv.SelectedItems.Add(item);
            }
        }
    }
}