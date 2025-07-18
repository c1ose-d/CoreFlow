namespace CoreFlow.Presentation.Controls;

public class Grid : System.Windows.Controls.Grid
{
    public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(nameof(Spacing), typeof(int), typeof(Grid), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public int Spacing
    {
        get => (int)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(Grid), new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    protected override Size MeasureOverride(Size constraint)
    {
        ColumnDefinitions.Clear();
        RowDefinitions.Clear();

        int count = Children.Count;
        if (count <= 1)
        {
            return base.MeasureOverride(constraint);
        }

        for (int i = 0; i < count; i++)
        {
            FrameworkElement child = (FrameworkElement)Children[i];
            if (Orientation == Orientation.Horizontal)
            {
                SetupHorizontal(child, i, count);
            }
            else
            {
                SetupVertical(child, i);
            }
        }

        return base.MeasureOverride(constraint);
    }

    private void SetupHorizontal(FrameworkElement child, int index, int count)
    {
        ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        SetColumn(child, index);

        if (child.HorizontalAlignment != HorizontalAlignment.Stretch)
        {
            child.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        child.Margin = GetHorizontalMargin(index, count);
    }

    private void SetupVertical(FrameworkElement child, int index)
    {
        RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        SetRow(child, index);

        double half = Spacing / 2.0;
        child.Margin = index == 0
            ? new Thickness(0, 0, 0, half)
            : new Thickness(0, half, 0, 0);
    }

    private Thickness GetHorizontalMargin(int index, int count)
    {
        double half = Spacing / 2.0;
        return index == 0
            ? new Thickness(0, 0, half, 0)
            : index == count - 1 ? new Thickness(half, 0, 0, 0) : new Thickness(half, 0, half, 0);
    }
}