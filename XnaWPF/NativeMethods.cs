namespace XnaWPF
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// An internal set of functionality used for interopping with native Win32 APIs.
    /// </summary>
    internal static class NativeMethods
    {
        #region Constants

        // http://msdn.microsoft.com/en-us/library/windows/desktop/ff700542(v=vs.85).aspx
        // http://msdn.microsoft.com/en-us/library/windows/desktop/ff468877(v=vs.85).aspx

        public const int WS_CHILD           = 0x40000000;
        public const int WS_VISIBLE         = 0x10000000;
        public const int WS_TABSTOP         = 0x00010000;

        public const int WS_EX_ACCEPTFILES  = 0x00000010;

        public const int WM_MOUSEMOVE       = 0x0200;
        public const int WM_LBUTTONDOWN     = 0x0201;
        public const int WM_LBUTTONUP       = 0x0202;
        public const int WM_LBUTTONDBLCLK   = 0x0203;
        public const int WM_RBUTTONDOWN     = 0x0204;
        public const int WM_RBUTTONUP       = 0x0205;
        public const int WM_RBUTTONDBLCLK   = 0x0206;
        public const int WM_MBUTTONDOWN     = 0x0207;
        public const int WM_MBUTTONUP       = 0x0208;
        public const int WM_MBUTTONDBLCLK   = 0x0209;
        public const int WM_MOUSEWHEEL      = 0x020A;
        public const int WM_MOUSEHWHEEL     = 0x020E;
        public const int WM_XBUTTONDOWN     = 0x020B;
        public const int WM_XBUTTONUP       = 0x020C;
        public const int WM_XBUTTONDBLCLK   = 0x020D;
        public const int WM_MOUSELEAVE      = 0x02A3;
        
        public const int MK_XBUTTON1        = 0x020;
        public const int MK_XBUTTON2        = 0x040;

        public const int WM_KEYDOWN         = 0x100;
        public const int WM_KEYUP           = 0x101;
        public const int WM_CHAR            = 0x102;

        public const int IDC_ARROW          = 32512;

        public const uint TME_LEAVE         = 0x00000002;

        #endregion

        #region Delegates and Structs

        public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public static readonly WndProc DefaultWindowProc = DefWindowProc;

        [StructLayout(LayoutKind.Sequential)]
        public struct TRACKMOUSEEVENT
        {
            public int cbSize;
            public uint dwFlags;
            public IntPtr hWnd;
            public uint dwHoverTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WNDCLASSEX
        {
            public uint cbSize;
            public uint style;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public WndProc lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        #endregion

        #region DllImports

        [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateWindowEx(
            int exStyle,
            string className,
            string windowName,
            int style,
            int x, int y,
            int width, int height,
            IntPtr hwndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            [MarshalAs(UnmanagedType.AsAny)] object pvParam);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Auto)]
        public static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string module);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll")]
        public static extern int TrackMouseEvent(ref TRACKMOUSEEVENT lpEventTrack);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.U2)]
        public static extern short RegisterClassEx([In] ref WNDCLASSEX lpwcx);

        [DllImport("user32.dll")]
        public static extern int ShowCursor(bool bShow);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref POINT point);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern int ScreenToClient(IntPtr hWnd, ref POINT pt);

        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        #endregion

        #region Helpers
        
        public static int GetXLParam(int lParam) 
        {
            return LowWord(lParam);
        }

        public static int GetYLParam(int lParam) 
        {
            return HighWord(lParam);
        }

        public static int LowWord(int input)
        {
            return (int)(input & 0xffff);
        }

        public static int HighWord(int input)
        {
            return (int)(input >> 16);
        }

        #endregion
    }
}
