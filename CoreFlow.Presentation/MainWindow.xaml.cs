namespace CoreFlow.Presentation;

public partial class MainWindow : Window
{
    private const string Glyph = "\uEF90";

    public MainWindow()
    {
        InitializeComponent();
        UpdateIcon();
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        HwndSource src = (HwndSource)PresentationSource.FromVisual(this);
        src.AddHook(WndProc);
    }

    private nint WndProc(nint hwnd, int msg, nint wParam, nint lParam, ref bool handled)
    {
        const int WM_GETMINMAXINFO = 0x0024;
        const int WM_SETTINGCHANGE = 0x001A;

        if (msg == WM_GETMINMAXINFO)
        {
            AdjustMaximizedSizeAndPosition(hwnd, lParam);
            handled = true;
        }

        if (msg == WM_SETTINGCHANGE)
        {
            string? param = Marshal.PtrToStringUni(lParam);
            if (param is "ImmersiveColorSet" or "SystemUsesLightTheme")
            {
                _ = Dispatcher.BeginInvoke(UpdateIcon);
            }
        }

        return nint.Zero;
    }

    private static void AdjustMaximizedSizeAndPosition(nint hwnd, nint lParam)
    {
        NativeMethods.MINMAXINFO mmi = Marshal.PtrToStructure<NativeMethods.MINMAXINFO>(lParam);

        nint hMonitor = NativeMethods.MonitorFromWindow(hwnd);
        if (hMonitor != nint.Zero)
        {
            NativeMethods.MONITORINFO mi = new()
            { cbSize = (uint)Marshal.SizeOf<NativeMethods.MONITORINFO>() };

            if (NativeMethods.GetMonitorInfo(hMonitor, ref mi))
            {
                mmi.ptMaxPosition.x = mi.rcWork.Left - mi.rcMonitor.Left;
                mmi.ptMaxPosition.y = mi.rcWork.Top - mi.rcMonitor.Top;
                mmi.ptMaxSize.x = mi.rcWork.Right - mi.rcWork.Left;
                mmi.ptMaxSize.y = mi.rcWork.Bottom - mi.rcWork.Top;
            }
        }

        Marshal.StructureToPtr(mmi, lParam, false);
    }

    private void UpdateIcon()
    {
        bool light = ThemeHelper.IsSystemLight();
        Color color = light ? Color.FromArgb(228, 0, 0, 0) : Color.FromArgb(255, 255, 255, 255);

        Icon = IconHelper.MakeGlyphIcon(Glyph, color);
    }
}