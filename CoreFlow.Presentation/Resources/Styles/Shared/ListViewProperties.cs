namespace CoreFlow.Presentation.Resources.Styles.Shared;

public static class ListViewProperties
{
    public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached(
                "SelectedItems",
                typeof(IList),
                typeof(ListViewProperties),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnSelectedItemsChanged
                )
            );

    public static void SetSelectedItems(DependencyObject element, IList value)
        => element.SetValue(SelectedItemsProperty, value);

    public static IList GetSelectedItems(DependencyObject element)
        => (IList)element.GetValue(SelectedItemsProperty);

    // чтобы хранить handler’ы и потом их отписать
    private static readonly Dictionary<ListView, NotifyCollectionChangedEventHandler>
        _colChangedHandlers = new();

    private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ListView lv) return;

        // 2) UI→VM
        lv.SelectionChanged -= ListView_SelectionChanged;
        lv.SelectionChanged += ListView_SelectionChanged;

        // 3) VM→UI: отписываем старый
        if (_colChangedHandlers.TryGetValue(lv, out var oldHandler) &&
            e.OldValue is INotifyCollectionChanged oldCol)
        {
            oldCol.CollectionChanged -= oldHandler;
        }

        // 4) VM→UI: подписываем новый
        if (e.NewValue is INotifyCollectionChanged newCol)
        {
            NotifyCollectionChangedEventHandler handler = (s, args) => BoundCollectionChanged(lv, args);
            _colChangedHandlers[lv] = handler;
            newCol.CollectionChanged += handler;
        }

        // 5) Начальное залитие UI выбранными из VM
        lv.SelectedItems.Clear();
        if (e.NewValue is IEnumerable list)
        {
            foreach (var item in list)
                lv.SelectedItems.Add(item);
        }
    }

    private static void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var lv = (ListView)sender;
        if (GetSelectedItems(lv) is not IList bound) return;

        foreach (var item in e.RemovedItems)
            bound.Remove(item);
        foreach (var item in e.AddedItems)
            bound.Add(item);
    }

    private static void BoundCollectionChanged(ListView lv, NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems != null)
            foreach (var item in e.OldItems)
                lv.SelectedItems.Remove(item);
        if (e.NewItems != null)
            foreach (var item in e.NewItems)
                lv.SelectedItems.Add(item);
    }
}