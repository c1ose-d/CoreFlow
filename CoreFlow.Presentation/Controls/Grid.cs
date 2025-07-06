namespace CoreFlow.Presentation.Controls;

public class Grid : System.Windows.Controls.Grid
{
    public static readonly DependencyProperty SpacingProperty =
        DependencyProperty.Register(
            nameof(Spacing),
            typeof(int),
            typeof(Grid),
            new FrameworkPropertyMetadata(
                0,
                FrameworkPropertyMetadataOptions.AffectsMeasure));

    public int Spacing
    {
        get => (int)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register(
            nameof(Orientation),
            typeof(Orientation),
            typeof(Grid),
            new FrameworkPropertyMetadata(
                Orientation.Horizontal,
                FrameworkPropertyMetadataOptions.AffectsMeasure));

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

        if (count > 1)
        {
            for (int i = 0; i < count; i++)
            {
                FrameworkElement child = (FrameworkElement)Children[i];

                if (Orientation == Orientation.Horizontal)
                {
                    ColumnDefinitions.Add(
                        new ColumnDefinition()
                        {
                            Width = new GridLength(
                                1,
                                GridUnitType.Star)
                        });
                    SetColumn(child, i);

                    if (child.HorizontalAlignment != HorizontalAlignment.Stretch)
                    {
                        child.HorizontalAlignment = HorizontalAlignment.Stretch;
                    }

                    if (i == 0)
                    {
                        child.Margin = new Thickness(
                            0,
                            0,
                            (double)Spacing / 2,
                            0);
                    }
                    else
                    {
                        child.Margin = i == count - 1
                            ? new Thickness(
                            (double)Spacing / 2,
                            0,
                            0,
                            0)
                            : new Thickness(
                            (double)Spacing / 2,
                            0,
                            (double)Spacing / 2,
                            0);
                    }
                }
                else
                {
                    RowDefinitions.Add(
                        new RowDefinition()
                        {
                            Height = GridLength.Auto
                        });
                    SetRow(child, i);

                    child.Margin = i == 0
                        ? new Thickness(
                            0,
                            0,
                            0,
                            (double)Spacing / 2)
                        : new Thickness(
                            0,
                            (double)Spacing / 2,
                            0,
                            0);
                }
            }
        }

        return base.MeasureOverride(constraint);
    }
}