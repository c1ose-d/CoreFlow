namespace CoreFlow.Presentation.Interop;

internal static class IconHelper
{
    public static ImageSource MakeGlyphIcon(string glyph, Color color, int px = 32)
    {
        DrawingVisual dv = new();
        using (DrawingContext dc = dv.RenderOpen())
        {
            FormattedText ft = new(glyph, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface(new FontFamily("Segoe Fluent Icons"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal), px - 4, new SolidColorBrush(color), VisualTreeHelper.GetDpi(dv).PixelsPerDip);

            Point origin = new((px - ft.Width) / 2, (px - ft.Height) / 2);

            dc.DrawText(ft, origin);
        }

        RenderTargetBitmap rtb = new(px, px, 96, 96, PixelFormats.Pbgra32);
        rtb.Render(dv);
        rtb.Freeze();
        return rtb;
    }
}