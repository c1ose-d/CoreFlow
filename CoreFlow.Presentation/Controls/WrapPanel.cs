namespace CoreFlow.Presentation.Controls;

public class WrapPanel : System.Windows.Controls.WrapPanel
{
    public double MinItemWidth
    {
        get => (double)GetValue(MinItemWidthProperty);
        set => SetValue(MinItemWidthProperty, value);
    }
    public static readonly DependencyProperty MinItemWidthProperty = DependencyProperty.Register(nameof(MinItemWidth), typeof(double), typeof(WrapPanel), new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    protected override Size MeasureOverride(Size availableSize)
    {
        if (double.IsInfinity(availableSize.Width) || MinItemWidth <= 0)
        {
            return base.MeasureOverride(availableSize);
        }

        int columns = Math.Max(1, (int)(availableSize.Width / MinItemWidth));

        ItemWidth = Math.Floor(availableSize.Width / columns);

        return base.MeasureOverride(availableSize);
    }
}