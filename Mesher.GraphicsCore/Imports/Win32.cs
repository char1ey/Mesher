using System;
using System.Runtime.InteropServices;

namespace Mesher.GraphicsCore
{
    internal static class Win32
    {
        /// <summary>
        /// Initializes the <see cref="Win32"/> class.
        /// </summary>
        static Win32()
        {
            //  Load the openGL library - without this wgl calls will fail.
            IntPtr glLibrary = LoadLibrary(OpenGL32);
        }

        //  The names of the libraries we're importing.
        public const String Kernel32 = "kernel32.dll";
        public const String OpenGL32 = "opengl32.dll";
        public const String Glu32 = "Glu32.dll";
        public const String Gdi32 = "gdi32.dll";
        public const String User32 = "user32.dll";

        #region Kernel32 Functions

        [DllImport(Kernel32, SetLastError = true)]
        public static extern IntPtr LoadLibrary(String lpFileName);

        #endregion

        #region WGL Functions

        /// <summary>
        /// Gets the current render context.
        /// </summary>
        /// <returns>The current render context.</returns>
        [DllImport(OpenGL32, SetLastError = true)]
        public static extern IntPtr wglGetCurrentContext();

        /// <summary>
        /// Gets the current device context.
        /// </summary>
        /// <returns>The current device context.</returns>
        [DllImport(OpenGL32, SetLastError = true)]
        public static extern IntPtr wglGetCurrentDC();

        /// <summary>
        /// Make the specified render context current.
        /// </summary>
        /// <param name="hdc">The handle to the device context.</param>
        /// <param name="hrc">The handle to the render context.</param>
        /// <returns></returns>
        [DllImport(OpenGL32, SetLastError = true)]
        public static extern Int32 wglMakeCurrent(IntPtr hdc, IntPtr hrc);

        /// <summary>
        /// Creates a render context from the device context.
        /// </summary>
        /// <param name="hdc">The handle to the device context.</param>
        /// <returns>The handle to the render context.</returns>
        [DllImport(OpenGL32, SetLastError = true)]
        public static extern IntPtr wglCreateContext(IntPtr hdc);

        /// <summary>
        /// Deletes the render context.
        /// </summary>
        /// <param name="hrc">The handle to the render context.</param>
        /// <returns></returns>
        [DllImport(OpenGL32, SetLastError = true)]
        public static extern Int32 wglDeleteContext(IntPtr hrc);

        /// <summary>
        /// Gets a proc address.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>The address of the function.</returns>
        [DllImport(OpenGL32, SetLastError = true)]
        public static extern IntPtr wglGetProcAddress(String name);

        /// <summary>
        /// The wglUseFontBitmaps function creates a set of bitmap display lists for use in the current OpenGL rendering context. The set of bitmap display lists is based on the glyphs in the currently selected font in the device context. You can then use bitmaps to draw characters in an OpenGL image.
        /// </summary>
        /// <param name="hDC">Specifies the device context whose currently selected font will be used to form the glyph bitmap display lists in the current OpenGL rendering context..</param>
        /// <param name="first">Specifies the first glyph in the run of glyphs that will be used to form glyph bitmap display lists.</param>
        /// <param name="count">Specifies the number of glyphs in the run of glyphs that will be used to form glyph bitmap display lists. The function creates count display lists, one for each glyph in the run.</param>
        /// <param name="listBase">Specifies a starting display list.</param>
        /// <returns>If the function succeeds, the return value is TRUE. If the function fails, the return value is FALSE. To get extended error information, call GetLastError.</returns>
        [DllImport(OpenGL32, SetLastError = true)]
        public static extern Boolean wglUseFontBitmaps(IntPtr hDC, UInt32 first, UInt32 count, UInt32 listBase);

        /// <summary>
        /// Link two render contexts so they share lists (buffer IDs, etc.)
        /// </summary>
        /// <param name="hrc1">The first context.</param>
        /// <param name="hrc2">The second context.</param>
        /// <returns>If the function succeeds, the return value is TRUE. If the function fails, the return value is FALSE. 
        /// To get extended error information, call GetLastError.</returns>
        [DllImport(OpenGL32, SetLastError = true)]
        public static extern Boolean wglShareLists(IntPtr hrc1, IntPtr hrc2);

        #endregion

        #region GL Functions
        [DllImport(OpenGL32, SetLastError = true)]
        public static extern void glViewport(Int32 x, Int32 y, Int32 width, Int32 height);
        #endregion

        #region PixelFormatDescriptor structure and flags.

        [StructLayout(LayoutKind.Explicit)]
        public class PIXELFORMATDESCRIPTOR
        {
            [FieldOffset(0)]
            public UInt16 nSize;
            [FieldOffset(2)]
            public UInt16 nVersion;
            [FieldOffset(4)]
            public UInt32 dwFlags;
            [FieldOffset(8)]
            public Byte iPixelType;
            [FieldOffset(9)]
            public Byte cColorBits;
            [FieldOffset(10)]
            public Byte cRedBits;
            [FieldOffset(11)]
            public Byte cRedShift;
            [FieldOffset(12)]
            public Byte cGreenBits;
            [FieldOffset(13)]
            public Byte cGreenShift;
            [FieldOffset(14)]
            public Byte cBlueBits;
            [FieldOffset(15)]
            public Byte cBlueShift;
            [FieldOffset(16)]
            public Byte cAlphaBits;
            [FieldOffset(17)]
            public Byte cAlphaShift;
            [FieldOffset(18)]
            public Byte cAccumBits;
            [FieldOffset(19)]
            public Byte cAccumRedBits;
            [FieldOffset(20)]
            public Byte cAccumGreenBits;
            [FieldOffset(21)]
            public Byte cAccumBlueBits;
            [FieldOffset(22)]
            public Byte cAccumAlphaBits;
            [FieldOffset(23)]
            public Byte cDepthBits;
            [FieldOffset(24)]
            public Byte cStencilBits;
            [FieldOffset(25)]
            public Byte cAuxBuffers;
            [FieldOffset(26)]
            public SByte iLayerType;
            [FieldOffset(27)]
            public Byte bReserved;
            [FieldOffset(28)]
            public UInt32 dwLayerMask;
            [FieldOffset(32)]
            public UInt32 dwVisibleMask;
            [FieldOffset(36)]
            public UInt32 dwDamageMask;
        }

        public struct PixelFormatDescriptor
        {
            public UInt16 nSize;
            public UInt16 nVersion;
            public UInt32 dwFlags;
            public Byte iPixelType;
            public Byte cColorBits;
            public Byte cRedBits;
            public Byte cRedShift;
            public Byte cGreenBits;
            public Byte cGreenShift;
            public Byte cBlueBits;
            public Byte cBlueShift;
            public Byte cAlphaBits;
            public Byte cAlphaShift;
            public Byte cAccumBits;
            public Byte cAccumRedBits;
            public Byte cAccumGreenBits;
            public Byte cAccumBlueBits;
            public Byte cAccumAlphaBits;
            public Byte cDepthBits;
            public Byte cStencilBits;
            public Byte cAuxBuffers;
            public SByte iLayerType;
            public Byte bReserved;
            public UInt32 dwLayerMask;
            public UInt32 dwVisibleMask;
            public UInt32 dwDamageMask;
        }

        public const Byte PFD_TYPE_RGBA = 0;
        public const Byte PFD_TYPE_COLORINDEX = 1;

        public const UInt32 PFD_DOUBLEBUFFER = 1;
        public const UInt32 PFD_STEREO = 2;
        public const UInt32 PFD_DRAW_TO_WINDOW = 4;
        public const UInt32 PFD_DRAW_TO_BITMAP = 8;
        public const UInt32 PFD_SUPPORT_GDI = 16;
        public const UInt32 PFD_SUPPORT_OPENGL = 32;
        public const UInt32 PFD_GENERIC_FORMAT = 64;
        public const UInt32 PFD_NEED_PALETTE = 128;
        public const UInt32 PFD_NEED_SYSTEM_PALETTE = 256;
        public const UInt32 PFD_SWAP_EXCHANGE = 512;
        public const UInt32 PFD_SWAP_COPY = 1024;
        public const UInt32 PFD_SWAP_LAYER_BUFFERS = 2048;
        public const UInt32 PFD_GENERIC_ACCELERATED = 4096;
        public const UInt32 PFD_SUPPORT_DIRECTDRAW = 8192;

        public const SByte PFD_MAIN_PLANE = 0;
        public const SByte PFD_OVERLAY_PLANE = 1;
        public const SByte PFD_UNDERLAY_PLANE = -1;

        public delegate IntPtr WndProc(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport(User32, SetLastError = true)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, UInt32 uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport(User32, SetLastError = true)]
        public static extern IntPtr CreateWindowEx(
           WindowStylesEx dwExStyle,
           String lpClassName,
           String lpWindowName,
           WindowStyles dwStyle,
           Int32 x,
           Int32 y,
           Int32 nWidth,
           Int32 nHeight,
           IntPtr hWndParent,
           IntPtr hMenu,
           IntPtr hInstance,
           IntPtr lpParam);

        [Flags]
        public enum WindowStylesEx : uint
        {
            /// <summary>
            /// Specifies that a window created with this style accepts drag-drop files.
            /// </summary>
            WS_EX_ACCEPTFILES = 0x00000010,
            /// <summary>
            /// Forces a top-level window onto the taskbar when the window is visible.
            /// </summary>
            WS_EX_APPWINDOW = 0x00040000,
            /// <summary>
            /// Specifies that a window has a border with a sunken edge.
            /// </summary>
            WS_EX_CLIENTEDGE = 0x00000200,
            /// <summary>
            /// Windows XP: Paints all descendants of a window in bottom-to-top painting order using double-buffering. For more information, see Remarks. This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. 
            /// </summary>
            WS_EX_COMPOSITED = 0x02000000,
            /// <summary>
            /// Includes a question mark in the title bar of the window. When the user clicks the question mark, the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message. The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command. The Help application displays a pop-up window that typically contains help for the child window.
            /// WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
            /// </summary>
            WS_EX_CONTEXTHELP = 0x00000400,
            /// <summary>
            /// The window itself contains child windows that should take part in dialog box navigation. If this style is specified, the dialog context recurses into children of this window when performing navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.
            /// </summary>
            WS_EX_CONTROLPARENT = 0x00010000,
            /// <summary>
            /// Creates a window that has a double border; the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.
            /// </summary>
            WS_EX_DLGMODALFRAME = 0x00000001,
            /// <summary>
            /// Windows 2000/XP: Creates a layered window. Note that this cannot be used for child windows. Also, this cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. 
            /// </summary>
            WS_EX_LAYERED = 0x00080000,
            /// <summary>
            /// Arabic and Hebrew versions of Windows 98/Me, Windows 2000/XP: Creates a window whose horizontal origin is on the right edge. Increasing horizontal values advance to the left. 
            /// </summary>
            WS_EX_LAYOUTRTL = 0x00400000,
            /// <summary>
            /// Creates a window that has generic left-aligned properties. This is the default.
            /// </summary>
            WS_EX_LEFT = 0x00000000,
            /// <summary>
            /// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical scroll bar (if present) is to the left of the client area. For other languages, the style is ignored.
            /// </summary>
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            /// <summary>
            /// The window text is displayed using left-to-right reading-order properties. This is the default.
            /// </summary>
            WS_EX_LTRREADING = 0x00000000,
            /// <summary>
            /// Creates a multiple-document interface (MDI) child window.
            /// </summary>
            WS_EX_MDICHILD = 0x00000040,
            /// <summary>
            /// Windows 2000/XP: A top-level window created with this style does not become the foreground window when the user clicks it. The system does not bring this window to the foreground when the user minimizes or closes the foreground window. 
            /// To activate the window, use the SetActiveWindow or SetForegroundWindow function.
            /// The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
            /// </summary>
            WS_EX_NOACTIVATE = 0x08000000,
            /// <summary>
            /// Windows 2000/XP: A window created with this style does not pass its window layout to its child windows.
            /// </summary>
            WS_EX_NOINHERITLAYOUT = 0x00100000,
            /// <summary>
            /// Specifies that a child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
            /// </summary>
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            /// <summary>
            /// Combines the WS_EX_CLIENTEDGE and WS_EX_WINDOWEDGE styles.
            /// </summary>
            WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,
            /// <summary>
            /// Combines the WS_EX_WINDOWEDGE, WS_EX_TOOLWINDOW, and WS_EX_TOPMOST styles.
            /// </summary>
            WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
            /// <summary>
            /// The window has generic "right-aligned" properties. This depends on the window class. This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the style is ignored.
            /// Using the WS_EX_RIGHT style for static or edit controls has the same effect as using the SS_RIGHT or ES_RIGHT style, respectively. Using this style with button controls has the same effect as using BS_RIGHT and BS_RIGHTBUTTON styles.
            /// </summary>
            WS_EX_RIGHT = 0x00001000,
            /// <summary>
            /// Vertical scroll bar (if present) is to the right of the client area. This is the default.
            /// </summary>
            WS_EX_RIGHTSCROLLBAR = 0x00000000,
            /// <summary>
            /// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text is displayed using right-to-left reading-order properties. For other languages, the style is ignored.
            /// </summary>
            WS_EX_RTLREADING = 0x00002000,
            /// <summary>
            /// Creates a window with a three-dimensional border style intended to be used for items that do not accept user input.
            /// </summary>
            WS_EX_STATICEDGE = 0x00020000,
            /// <summary>
            /// Creates a tool window; that is, a window intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font. A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, its icon is not displayed on the title bar. However, you can display the system menu by right-clicking or by typing ALT+SPACE. 
            /// </summary>
            WS_EX_TOOLWINDOW = 0x00000080,
            /// <summary>
            /// Specifies that a window created with this style should be placed above all non-topmost windows and should stay above them, even when the window is deactivated. To add or remove this style, use the SetWindowPos function.
            /// </summary>
            WS_EX_TOPMOST = 0x00000008,
            /// <summary>
            /// Specifies that a window created with this style should not be painted until siblings beneath the window (that were created by the same thread) have been painted. The window appears transparent because the bits of underlying sibling windows have already been painted.
            /// To achieve transparency without these restrictions, use the SetWindowRgn function.
            /// </summary>
            WS_EX_TRANSPARENT = 0x00000020,
            /// <summary>
            /// Specifies that a window has a border with a raised edge.
            /// </summary>
            WS_EX_WINDOWEDGE = 0x00000100
        }

        [Flags()]
        public enum WindowStyles : uint
        {
            /// <summary>The window has a thin-line border.</summary>
            WS_BORDER = 0x800000,

            /// <summary>The window has a title bar (includes the WS_BORDER style).</summary>
            WS_CAPTION = 0xc00000,

            /// <summary>The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.</summary>
            WS_CHILD = 0x40000000,

            /// <summary>Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.</summary>
            WS_CLIPCHILDREN = 0x2000000,

            /// <summary>
            /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated.
            /// If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
            /// </summary>
            WS_CLIPSIBLINGS = 0x4000000,

            /// <summary>The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.</summary>
            WS_DISABLED = 0x8000000,

            /// <summary>The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.</summary>
            WS_DLGFRAME = 0x400000,

            /// <summary>
            /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style.
            /// The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
            /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
            /// </summary>
            WS_GROUP = 0x20000,

            /// <summary>The window has a horizontal scroll bar.</summary>
            WS_HSCROLL = 0x100000,

            /// <summary>The window is initially maximized.</summary> 
            WS_MAXIMIZE = 0x1000000,

            /// <summary>The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary> 
            WS_MAXIMIZEBOX = 0x10000,

            /// <summary>The window is initially minimized.</summary>
            WS_MINIMIZE = 0x20000000,

            /// <summary>The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
            WS_MINIMIZEBOX = 0x20000,

            /// <summary>The window is an overlapped window. An overlapped window has a title bar and a border.</summary>
            WS_OVERLAPPED = 0x0,

            /// <summary>The window is an overlapped window.</summary>
            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

            /// <summary>The window is a pop-up window. This style cannot be used with the WS_CHILD style.</summary>
            WS_POPUP = 0x80000000u,

            /// <summary>The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.</summary>
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

            /// <summary>The window has a sizing border.</summary>
            WS_SIZEFRAME = 0x40000,

            /// <summary>The window has a window menu on its title bar. The WS_CAPTION style must also be specified.</summary>
            WS_SYSMENU = 0x80000,

            /// <summary>
            /// The window is a control that can receive the keyboard focus when the user presses the TAB key.
            /// Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.  
            /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
            /// For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
            /// </summary>
            WS_TABSTOP = 0x10000,

            /// <summary>The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.</summary>
            WS_VISIBLE = 0x10000000,

            /// <summary>The window has a vertical scroll bar.</summary>
            WS_VSCROLL = 0x200000
        }


        [Flags]
        public enum ClassStyles : uint
        {
            ByteAlignClient = 0x1000,
            ByteAlignWindow = 0x2000,
            ClassDC = 0x40,
            DoubleClicks = 0x8,
            DropShadow = 0x20000,
            GlobalClass = 0x4000,
            HorizontalRedraw = 0x2,
            NoClose = 0x200,
            OwnDC = 0x20,
            ParentDC = 0x80,
            SaveBits = 0x800,
            VerticalRedraw = 0x1
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WNDCLASSEX
        {
            public UInt32 cbSize;
            public ClassStyles style;
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public WndProc lpfnWndProc;
            public Int32 cbClsExtra;
            public Int32 cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public String lpszMenuName;
            public String lpszClassName;
            public IntPtr hIconSm;

            public void Init()
            {
                cbSize = (UInt32)Marshal.SizeOf(this);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            public Int32 biSize;
            public Int32 biWidth;
            public Int32 biHeight;
            public Int16 biPlanes;
            public Int16 biBitCount;
            public Int32 biCompression;
            public Int32 biSizeImage;
            public Int32 biXPelsPerMeter;
            public Int32 biYPelsPerMeter;
            public Int32 biClrUsed;
            public Int32 biClrImportant;

            public void Init()
            {
                biSize = Marshal.SizeOf(this);
            }
        }

        #endregion

        #region Win32 Function Definitions.



        //	Unmanaged functions from the Win32 graphics library.
        [DllImport(Gdi32, SetLastError = true)]
        public unsafe static extern Int32 ChoosePixelFormat(IntPtr hDC,
            [In, MarshalAs(UnmanagedType.LPStruct)] PIXELFORMATDESCRIPTOR ppfd);

        [DllImport(Gdi32, SetLastError = true)]
        public unsafe static extern Int32 SetPixelFormat(IntPtr hDC, Int32 iPixelFormat,
            [In, MarshalAs(UnmanagedType.LPStruct)] PIXELFORMATDESCRIPTOR ppfd);

        [DllImport(Gdi32, SetLastError = true)]
        public static extern IntPtr GetStockObject(UInt32 fnObject);

        [DllImport(Gdi32, SetLastError = true)]
        public static extern Int32 SwapBuffers(IntPtr hDC);

        [DllImport(Gdi32, SetLastError = true)]
        public static extern Boolean BitBlt(IntPtr hDC, Int32 x, Int32 y, Int32 width,
            Int32 height, IntPtr hDCSource, Int32 sourceX, Int32 sourceY, UInt32 type);

        [DllImport(Gdi32, SetLastError = true)]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BITMAPINFO pbmi,
           UInt32 pila, out IntPtr ppvBits, IntPtr hSection, UInt32 dwOffset);

        [DllImport(Gdi32, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport(Gdi32, SetLastError = true)]
        public static extern Boolean DeleteObject(IntPtr hObject);

        [DllImport(Gdi32, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport(Gdi32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean DeleteDC(IntPtr hDC);

        [DllImport(Gdi32, SetLastError = true)]
        public static extern IntPtr CreateFont(Int32 nHeight, Int32 nWidth, Int32 nEscapement,
           Int32 nOrientation, UInt32 fnWeight, UInt32 fdwItalic, UInt32 fdwUnderline, UInt32
           fdwStrikeOut, UInt32 fdwCharSet, UInt32 fdwOutputPrecision, UInt32
           fdwClipPrecision, UInt32 fdwQuality, UInt32 fdwPitchAndFamily, String lpszFace);

        #endregion

        #region User32 Functions

        [DllImport(User32, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport(User32, SetLastError = true)]
        public static extern Int32 ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport(User32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean DestroyWindow(IntPtr hWnd);

        [DllImport(User32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, Int32 X, Int32 Y, Int32 cx, Int32 cy, SetWindowPosFlags uFlags);

        [DllImport(User32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U2)]
        public static extern Int16 RegisterClassEx([In] ref WNDCLASSEX lpwcx);

        #endregion

        [Flags]
        public enum SetWindowPosFlags : uint
        {
            SWP_ASYNCWINDOWPOS = 0x4000,
            SWP_DEFERERASE = 0x2000,
            SWP_DRAWFRAME = 0x0020,
            SWP_FRAMECHANGED = 0x0020,
            SWP_HIDEWINDOW = 0x0080,
            SWP_NOACTIVATE = 0x0010,
            SWP_NOCOPYBITS = 0x0100,
            SWP_NOMOVE = 0x0002,
            SWP_NOOWNERZORDER = 0x0200,
            SWP_NOREDRAW = 0x0008,
            SWP_NOREPOSITION = 0x0200,
            SWP_NOSENDCHANGING = 0x0400,
            SWP_NOSIZE = 0x0001,
            SWP_NOZORDER = 0x0004,
            SWP_SHOWWINDOW = 0x0040,
        }

        #region Windows Messages

        public const Int32 WM_ACTIVATE = 0x0006;
        public const Int32 WM_ACTIVATEAPP = 0x001C;
        public const Int32 WM_AFXFIRST = 0x0360;
        public const Int32 WM_AFXLAST = 0x037F;
        public const Int32 WM_APP = 0x8000;
        public const Int32 WM_ASKCBFORMATNAME = 0x030C;
        public const Int32 WM_CANCELJOURNAL = 0x004B;
        public const Int32 WM_CANCELMODE = 0x001F;
        public const Int32 WM_CAPTURECHANGED = 0x0215;
        public const Int32 WM_CHANGECBCHAIN = 0x030D;
        public const Int32 WM_CHANGEUISTATE = 0x0127;
        public const Int32 WM_CHAR = 0x0102;
        public const Int32 WM_CHARTOITEM = 0x002F;
        public const Int32 WM_CHILDACTIVATE = 0x0022;
        public const Int32 WM_CLEAR = 0x0303;
        public const Int32 WM_CLOSE = 0x0010;
        public const Int32 WM_COMMAND = 0x0111;
        public const Int32 WM_COMPACTING = 0x0041;
        public const Int32 WM_COMPAREITEM = 0x0039;
        public const Int32 WM_CONTEXTMENU = 0x007B;
        public const Int32 WM_COPY = 0x0301;
        public const Int32 WM_COPYDATA = 0x004A;
        public const Int32 WM_CREATE = 0x0001;
        public const Int32 WM_CTLCOLORBTN = 0x0135;
        public const Int32 WM_CTLCOLORDLG = 0x0136;
        public const Int32 WM_CTLCOLOREDIT = 0x0133;
        public const Int32 WM_CTLCOLORLISTBOX = 0x0134;
        public const Int32 WM_CTLCOLORMSGBOX = 0x0132;
        public const Int32 WM_CTLCOLORSCROLLBAR = 0x0137;
        public const Int32 WM_CTLCOLORSTATIC = 0x0138;
        public const Int32 WM_CUT = 0x0300;
        public const Int32 WM_DEADCHAR = 0x0103;
        public const Int32 WM_DELETEITEM = 0x002D;
        public const Int32 WM_DESTROY = 0x0002;
        public const Int32 WM_DESTROYCLIPBOARD = 0x0307;
        public const Int32 WM_DEVICECHANGE = 0x0219;
        public const Int32 WM_DEVMODECHANGE = 0x001B;
        public const Int32 WM_DISPLAYCHANGE = 0x007E;
        public const Int32 WM_DRAWCLIPBOARD = 0x0308;
        public const Int32 WM_DRAWITEM = 0x002B;
        public const Int32 WM_DROPFILES = 0x0233;
        public const Int32 WM_ENABLE = 0x000A;
        public const Int32 WM_ENDSESSION = 0x0016;
        public const Int32 WM_ENTERIDLE = 0x0121;
        public const Int32 WM_ENTERMENULOOP = 0x0211;
        public const Int32 WM_ENTERSIZEMOVE = 0x0231;
        public const Int32 WM_ERASEBKGND = 0x0014;
        public const Int32 WM_EXITMENULOOP = 0x0212;
        public const Int32 WM_EXITSIZEMOVE = 0x0232;
        public const Int32 WM_FONTCHANGE = 0x001D;
        public const Int32 WM_GETDLGCODE = 0x0087;
        public const Int32 WM_GETFONT = 0x0031;
        public const Int32 WM_GETHOTKEY = 0x0033;
        public const Int32 WM_GETICON = 0x007F;
        public const Int32 WM_GETMINMAXINFO = 0x0024;
        public const Int32 WM_GETOBJECT = 0x003D;
        public const Int32 WM_GETTEXT = 0x000D;
        public const Int32 WM_GETTEXTLENGTH = 0x000E;
        public const Int32 WM_HANDHELDFIRST = 0x0358;
        public const Int32 WM_HANDHELDLAST = 0x035F;
        public const Int32 WM_HELP = 0x0053;
        public const Int32 WM_HOTKEY = 0x0312;
        public const Int32 WM_HSCROLL = 0x0114;
        public const Int32 WM_HSCROLLCLIPBOARD = 0x030E;
        public const Int32 WM_ICONERASEBKGND = 0x0027;
        public const Int32 WM_IME_CHAR = 0x0286;
        public const Int32 WM_IME_COMPOSITION = 0x010F;
        public const Int32 WM_IME_COMPOSITIONFULL = 0x0284;
        public const Int32 WM_IME_CONTROL = 0x0283;
        public const Int32 WM_IME_ENDCOMPOSITION = 0x010E;
        public const Int32 WM_IME_KEYDOWN = 0x0290;
        public const Int32 WM_IME_KEYLAST = 0x010F;
        public const Int32 WM_IME_KEYUP = 0x0291;
        public const Int32 WM_IME_NOTIFY = 0x0282;
        public const Int32 WM_IME_REQUEST = 0x0288;
        public const Int32 WM_IME_SELECT = 0x0285;
        public const Int32 WM_IME_SETCONTEXT = 0x0281;
        public const Int32 WM_IME_STARTCOMPOSITION = 0x010D;
        public const Int32 WM_INITDIALOG = 0x0110;
        public const Int32 WM_INITMENU = 0x0116;
        public const Int32 WM_INITMENUPOPUP = 0x0117;
        public const Int32 WM_INPUTLANGCHANGE = 0x0051;
        public const Int32 WM_INPUTLANGCHANGEREQUEST = 0x0050;
        public const Int32 WM_KEYDOWN = 0x0100;
        public const Int32 WM_KEYFIRST = 0x0100;
        public const Int32 WM_KEYLAST = 0x0108;
        public const Int32 WM_KEYUP = 0x0101;
        public const Int32 WM_KILLFOCUS = 0x0008;
        public const Int32 WM_LBUTTONDBLCLK = 0x0203;
        public const Int32 WM_LBUTTONDOWN = 0x0201;
        public const Int32 WM_LBUTTONUP = 0x0202;
        public const Int32 WM_MBUTTONDBLCLK = 0x0209;
        public const Int32 WM_MBUTTONDOWN = 0x0207;
        public const Int32 WM_MBUTTONUP = 0x0208;
        public const Int32 WM_MDIACTIVATE = 0x0222;
        public const Int32 WM_MDICASCADE = 0x0227;
        public const Int32 WM_MDICREATE = 0x0220;
        public const Int32 WM_MDIDESTROY = 0x0221;
        public const Int32 WM_MDIGETACTIVE = 0x0229;
        public const Int32 WM_MDIICONARRANGE = 0x0228;
        public const Int32 WM_MDIMAXIMIZE = 0x0225;
        public const Int32 WM_MDINEXT = 0x0224;
        public const Int32 WM_MDIREFRESHMENU = 0x0234;
        public const Int32 WM_MDIRESTORE = 0x0223;
        public const Int32 WM_MDISETMENU = 0x0230;
        public const Int32 WM_MDITILE = 0x0226;
        public const Int32 WM_MEASUREITEM = 0x002C;
        public const Int32 WM_MENUCHAR = 0x0120;
        public const Int32 WM_MENUCOMMAND = 0x0126;
        public const Int32 WM_MENUDRAG = 0x0123;
        public const Int32 WM_MENUGETOBJECT = 0x0124;
        public const Int32 WM_MENURBUTTONUP = 0x0122;
        public const Int32 WM_MENUSELECT = 0x011F;
        public const Int32 WM_MOUSEACTIVATE = 0x0021;
        public const Int32 WM_MOUSEFIRST = 0x0200;
        public const Int32 WM_MOUSEHOVER = 0x02A1;
        public const Int32 WM_MOUSELAST = 0x020D;
        public const Int32 WM_MOUSELEAVE = 0x02A3;
        public const Int32 WM_MOUSEMOVE = 0x0200;
        public const Int32 WM_MOUSEWHEEL = 0x020A;
        public const Int32 WM_MOUSEHWHEEL = 0x020E;
        public const Int32 WM_MOVE = 0x0003;
        public const Int32 WM_MOVING = 0x0216;
        public const Int32 WM_NCACTIVATE = 0x0086;
        public const Int32 WM_NCCALCSIZE = 0x0083;
        public const Int32 WM_NCCREATE = 0x0081;
        public const Int32 WM_NCDESTROY = 0x0082;
        public const Int32 WM_NCHITTEST = 0x0084;
        public const Int32 WM_NCLBUTTONDBLCLK = 0x00A3;
        public const Int32 WM_NCLBUTTONDOWN = 0x00A1;
        public const Int32 WM_NCLBUTTONUP = 0x00A2;
        public const Int32 WM_NCMBUTTONDBLCLK = 0x00A9;
        public const Int32 WM_NCMBUTTONDOWN = 0x00A7;
        public const Int32 WM_NCMBUTTONUP = 0x00A8;
        public const Int32 WM_NCMOUSEMOVE = 0x00A0;
        public const Int32 WM_NCPAINT = 0x0085;
        public const Int32 WM_NCRBUTTONDBLCLK = 0x00A6;
        public const Int32 WM_NCRBUTTONDOWN = 0x00A4;
        public const Int32 WM_NCRBUTTONUP = 0x00A5;
        public const Int32 WM_NEXTDLGCTL = 0x0028;
        public const Int32 WM_NEXTMENU = 0x0213;
        public const Int32 WM_NOTIFY = 0x004E;
        public const Int32 WM_NOTIFYFORMAT = 0x0055;
        public const Int32 WM_NULL = 0x0000;
        public const Int32 WM_PAINT = 0x000F;
        public const Int32 WM_PAINTCLIPBOARD = 0x0309;
        public const Int32 WM_PAINTICON = 0x0026;
        public const Int32 WM_PALETTECHANGED = 0x0311;
        public const Int32 WM_PALETTEISCHANGING = 0x0310;
        public const Int32 WM_PARENTNOTIFY = 0x0210;
        public const Int32 WM_PASTE = 0x0302;
        public const Int32 WM_PENWINFIRST = 0x0380;
        public const Int32 WM_PENWINLAST = 0x038F;
        public const Int32 WM_POWER = 0x0048;
        public const Int32 WM_POWERBROADCAST = 0x0218;
        public const Int32 WM_PRINT = 0x0317;
        public const Int32 WM_PRINTCLIENT = 0x0318;
        public const Int32 WM_QUERYDRAGICON = 0x0037;
        public const Int32 WM_QUERYENDSESSION = 0x0011;
        public const Int32 WM_QUERYNEWPALETTE = 0x030F;
        public const Int32 WM_QUERYOPEN = 0x0013;
        public const Int32 WM_QUEUESYNC = 0x0023;
        public const Int32 WM_QUIT = 0x0012;
        public const Int32 WM_RBUTTONDBLCLK = 0x0206;
        public const Int32 WM_RBUTTONDOWN = 0x0204;
        public const Int32 WM_RBUTTONUP = 0x0205;
        public const Int32 WM_RENDERALLFORMATS = 0x0306;
        public const Int32 WM_RENDERFORMAT = 0x0305;
        public const Int32 WM_SETCURSOR = 0x0020;
        public const Int32 WM_SETFOCUS = 0x0007;
        public const Int32 WM_SETFONT = 0x0030;
        public const Int32 WM_SETHOTKEY = 0x0032;
        public const Int32 WM_SETICON = 0x0080;
        public const Int32 WM_SETREDRAW = 0x000B;
        public const Int32 WM_SETTEXT = 0x000C;
        public const Int32 WM_SETTINGCHANGE = 0x001A;
        public const Int32 WM_SHOWWINDOW = 0x0018;
        public const Int32 WM_SIZE = 0x0005;
        public const Int32 WM_SIZECLIPBOARD = 0x030B;
        public const Int32 WM_SIZING = 0x0214;
        public const Int32 WM_SPOOLERSTATUS = 0x002A;
        public const Int32 WM_STYLECHANGED = 0x007D;
        public const Int32 WM_STYLECHANGING = 0x007C;
        public const Int32 WM_SYNCPAINT = 0x0088;
        public const Int32 WM_SYSCHAR = 0x0106;
        public const Int32 WM_SYSCOLORCHANGE = 0x0015;
        public const Int32 WM_SYSCOMMAND = 0x0112;
        public const Int32 WM_SYSDEADCHAR = 0x0107;
        public const Int32 WM_SYSKEYDOWN = 0x0104;
        public const Int32 WM_SYSKEYUP = 0x0105;
        public const Int32 WM_TCARD = 0x0052;
        public const Int32 WM_TIMECHANGE = 0x001E;
        public const Int32 WM_TIMER = 0x0113;
        public const Int32 WM_UNDO = 0x0304;
        public const Int32 WM_UNINITMENUPOPUP = 0x0125;
        public const Int32 WM_USER = 0x0400;
        public const Int32 WM_USERCHANGED = 0x0054;
        public const Int32 WM_VKEYTOITEM = 0x002E;
        public const Int32 WM_VSCROLL = 0x0115;
        public const Int32 WM_VSCROLLCLIPBOARD = 0x030A;
        public const Int32 WM_WINDOWPOSCHANGED = 0x0047;
        public const Int32 WM_WINDOWPOSCHANGING = 0x0046;
        public const Int32 WM_WININICHANGE = 0x001A;
        public const Int32 WM_XBUTTONDBLCLK = 0x020D;
        public const Int32 WM_XBUTTONDOWN = 0x020B;
        public const Int32 WM_XBUTTONUP = 0x020C;

        #endregion

        public const UInt32 WHITE_BRUSH = 0;
        public const UInt32 LTGRAY_BRUSH = 1;
        public const UInt32 GRAY_BRUSH = 2;
        public const UInt32 DKGRAY_BRUSH = 3;
        public const UInt32 BLACK_BRUSH = 4;
        public const UInt32 NULL_BRUSH = 5;
        public const UInt32 HOLLOW_BRUSH = NULL_BRUSH;
        public const UInt32 WHITE_PEN = 6;
        public const UInt32 BLACK_PEN = 7;
        public const UInt32 NULL_PEN = 8;
        public const UInt32 OEM_FIXED_FONT = 10;
        public const UInt32 ANSI_FIXED_FONT = 11;
        public const UInt32 ANSI_VAR_FONT = 12;
        public const UInt32 SYSTEM_FONT = 13;
        public const UInt32 DEVICE_DEFAULT_FONT = 14;
        public const UInt32 DEFAULT_PALETTE = 15;
        public const UInt32 SYSTEM_FIXED_FONT = 16;
        public const UInt32 DEFAULT_GUI_FONT = 17;
        public const UInt32 DC_BRUSH = 18;
        public const UInt32 DC_PEN = 19;

        public const UInt32 DEFAULT_PITCH = 0;
        public const UInt32 FIXED_PITCH = 1;
        public const UInt32 VARIABLE_PITCH = 2;

        public const UInt32 DEFAULT_QUALITY = 0;
        public const UInt32 DRAFT_QUALITY = 1;
        public const UInt32 PROOF_QUALITY = 2;
        public const UInt32 NONANTIALIASED_QUALITY = 3;
        public const UInt32 ANTIALIASED_QUALITY = 4;
        public const UInt32 CLEARTYPE_QUALITY = 5;
        public const UInt32 CLEARTYPE_NATURAL_QUALITY = 6;

        public const UInt32 CLIP_DEFAULT_PRECIS = 0;
        public const UInt32 CLIP_CHARACTER_PRECIS = 1;
        public const UInt32 CLIP_STROKE_PRECIS = 2;
        public const UInt32 CLIP_MASK = 0xf;

        public const UInt32 OUT_DEFAULT_PRECIS = 0;
        public const UInt32 OUT_STRING_PRECIS = 1;
        public const UInt32 OUT_CHARACTER_PRECIS = 2;
        public const UInt32 OUT_STROKE_PRECIS = 3;
        public const UInt32 OUT_TT_PRECIS = 4;
        public const UInt32 OUT_DEVICE_PRECIS = 5;
        public const UInt32 OUT_RASTER_PRECIS = 6;
        public const UInt32 OUT_TT_ONLY_PRECIS = 7;
        public const UInt32 OUT_OUTLINE_PRECIS = 8;
        public const UInt32 OUT_SCREEN_OUTLINE_PRECIS = 9;
        public const UInt32 OUT_PS_ONLY_PRECIS = 10;

        public const UInt32 ANSI_CHARSET = 0;
        public const UInt32 DEFAULT_CHARSET = 1;
        public const UInt32 SYMBOL_CHARSET = 2;

        public const UInt32 FW_DONTCARE = 0;
        public const UInt32 FW_THIN = 100;
        public const UInt32 FW_EXTRALIGHT = 200;
        public const UInt32 FW_LIGHT = 300;
        public const UInt32 FW_NORMAL = 400;
        public const UInt32 FW_MEDIUM = 500;
        public const UInt32 FW_SEMIBOLD = 600;
        public const UInt32 FW_BOLD = 700;
        public const UInt32 FW_EXTRABOLD = 800;
        public const UInt32 FW_HEAVY = 900;

        public const UInt32 SRCCOPY = 0x00CC0020; // dest = source                   
        public const UInt32 SRCPAINT = 0x00EE0086;    // dest = source OR dest           
        public const UInt32 SRCAND = 0x008800C6;  // dest = source AND dest          
        public const UInt32 SRCINVERT = 0x00660046;   // dest = source XOR dest          
        public const UInt32 SRCERASE = 0x00440328;    // dest = source AND (NOT dest )   
        public const UInt32 NOTSRCCOPY = 0x00330008;  // dest = (NOT source)             
        public const UInt32 NOTSRCERASE = 0x001100A6; // dest = (NOT src) AND (NOT dest) 
        public const UInt32 MERGECOPY = 0x00C000CA;   // dest = (source AND pattern)     
        public const UInt32 MERGEPAINT = 0x00BB0226;  // dest = (NOT source) OR dest     
        public const UInt32 PATCOPY = 0x00F00021; // dest = pattern                  
        public const UInt32 PATPAINT = 0x00FB0A09;    // dest = DPSnoo                   
        public const UInt32 PATINVERT = 0x005A0049;   // dest = pattern XOR dest         
        public const UInt32 DSTINVERT = 0x00550009;   // dest = (NOT dest)               
        public const UInt32 BLACKNESS = 0x00000042;   // dest = BLACK                    
        public const UInt32 WHITENESS = 0x00FF0062;   // dest = WHITE     

        public const UInt32 DIB_RGB_COLORS = 0;
        public const UInt32 DIB_PAL_COLORS = 1;

        public const UInt32 CS_VREDRAW = 0x0001;
        public const UInt32 CS_HREDRAW = 0x0002;
        public const UInt32 CS_DBLCLKS = 0x0008;
        public const UInt32 CS_OWNDC = 0x0020;
        public const UInt32 CS_CLASSDC = 0x0040;
        public const UInt32 CS_PARENTDC = 0x0080;
        public const UInt32 CS_NOCLOSE = 0x0200;
        public const UInt32 CS_SAVEBITS = 0x0800;
        public const UInt32 CS_BYTEALIGNCLIENT = 0x1000;
        public const UInt32 CS_BYTEALIGNWINDOW = 0x2000;
        public const UInt32 CS_GLOBALCLASS = 0x4000;
    }
}
