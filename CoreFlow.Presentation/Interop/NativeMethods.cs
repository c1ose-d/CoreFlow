namespace CoreFlow.Presentation.Interop;

internal static partial class NativeMethods
{
    private const string USER32 = "user32.dll";
    internal const int MONITOR_DEFAULTTONEAREST = 0x00000002;

    [LibraryImport(USER32, SetLastError = false)]
    internal static partial nint MonitorFromWindow(nint hwnd, int dwFlags = MONITOR_DEFAULTTONEAREST);

    [LibraryImport(USER32, EntryPoint = "GetMonitorInfoW", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetMonitorInfo(nint hMonitor, ref MonitorInfo lpmi);

    [StructLayout(LayoutKind.Sequential)]
    internal struct Point
    {
        public int x, y;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MinMaxInfo
    {
        public Point ptReserved;
        public Point ptMaxSize;
        public Point ptMaxPosition;
        public Point ptMinTrackSize;
        public Point ptMaxTrackSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Rect
    {
        public int Left, Top, Right, Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MonitorInfo
    {
        public uint cbSize;
        public Rect rcMonitor;
        public Rect rcWork;
        public uint dwFlags;
    }
}