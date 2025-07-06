namespace CoreFlow.Presentation.Interop;

internal static partial class NativeMethods
{
    private const string USER32 = "user32.dll";
    internal const int MONITOR_DEFAULTTONEAREST = 0x00000002;

    [LibraryImport(USER32, SetLastError = false)]
    internal static partial nint MonitorFromWindow(nint hwnd, int dwFlags = MONITOR_DEFAULTTONEAREST);

    [LibraryImport(USER32, EntryPoint = "GetMonitorInfoW", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetMonitorInfo(nint hMonitor, ref MONITORINFO lpmi);

    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        public int x, y;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MINMAXINFO
    {
        public POINT ptReserved;
        public POINT ptMaxSize;
        public POINT ptMaxPosition;
        public POINT ptMinTrackSize;
        public POINT ptMaxTrackSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int Left, Top, Right, Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MONITORINFO
    {
        public uint cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public uint dwFlags;
    }
}