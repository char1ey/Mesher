using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Mesher.OpenGLCore;

namespace Mesher.Components
{
    /// <summary>
    /// Логика взаимодействия для RenderContext.xaml
    /// </summary>
    public partial class RenderContext : System.Windows.Controls.UserControl, IDisposable
    {
        private GlWindow m_GlWindow;

        public event EventHandler ContextResize;

        new public event MouseEventHandler MouseMove;
        new public event MouseEventHandler MouseWheel;

        new public event System.Windows.Input.KeyEventHandler KeyUp;
        new public event System.Windows.Input.KeyEventHandler KeyDown;

        public RenderContext()
        {
            InitializeComponent();

            windowsFormsHost.Child = new Control();

            m_GlWindow = new GlWindow(windowsFormsHost.Child.Handle);

            windowsFormsHost.Child.MouseWheel += Child_MouseWheel;
            windowsFormsHost.Child.MouseMove += Child_MouseMove;

            windowsFormsHost.KeyDown += WindowsFormsHost_KeyDown;
            windowsFormsHost.KeyUp += WindowsFormsHost_KeyUp;

            windowsFormsHost.Child.Resize += Child_Resize;
        }

        private void Child_MouseWheel(object sender, MouseEventArgs e)
        {
            MouseWheel?.Invoke(sender, e);
        }

        private void WindowsFormsHost_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyUp?.Invoke(sender, e);
        }

        private void WindowsFormsHost_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyDown?.Invoke(sender, e);
        }

        private void Child_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove?.Invoke(sender, e);
        }

        private void Child_Resize(object sender, EventArgs e)
        {
            var control = (Control)sender;
            m_GlWindow.ResizeWindow(control.Width, control.Height);

            ContextResize?.Invoke(sender, e);
        }

        public void BeginRender()
        {
            m_GlWindow.Begin();
        }

        public void EndRender()
        {
            m_GlWindow.SwapBuffers();
            m_GlWindow.End();
        }

        public void Dispose()
        {
            m_GlWindow.Dispose();
        }

        class GlWindow : NativeWindow, IDisposable
        {
            private IntPtr m_HDC;
            private IntPtr m_Hglrc;

            private IntPtr m_PreviousHDC;
            private IntPtr m_PreviousHglrc;

            public GlWindow(IntPtr handle)
            {
                var createParams = new CreateParams
                {
                    Parent = handle,
                    Style = (int)(Win32.WindowStyles.WS_CHILD | Win32.WindowStyles.WS_VISIBLE | Win32.WindowStyles.WS_DISABLED)
                };

                CreateHandle(createParams);

                m_HDC = Win32.GetDC(Handle);

                if (m_HDC != null)
                {
                    var pfd = new Win32.PIXELFORMATDESCRIPTOR()
                    {
                        nSize = (ushort)Marshal.SizeOf<Win32.PIXELFORMATDESCRIPTOR>(),
                        nVersion = 1,
                        dwFlags = Win32.PFD_DRAW_TO_WINDOW | Win32.PFD_SUPPORT_OPENGL | Win32.PFD_DOUBLEBUFFER,
                        iPixelType = Win32.PFD_TYPE_RGBA,
                        cColorBits = 32,
                        cDepthBits = 24,
                        cStencilBits = 8,
                        iLayerType = Win32.PFD_MAIN_PLANE
                    };
                    int pixelFrormat = Win32.ChoosePixelFormat(m_HDC, pfd);
                    Win32.SetPixelFormat(m_HDC, pixelFrormat, pfd);
                    m_Hglrc = Win32.wglCreateContext(m_HDC);
                    Win32.wglMakeCurrent(m_HDC, m_Hglrc);
                }
            }

            public void ResizeWindow(int width, int height)
            {
                Begin();
                Win32.glViewport(0, 0, width, height);
                End();
                Win32.SetWindowPos(Handle, IntPtr.Zero, 0, 0, width, height, Win32.SetWindowPosFlags.SWP_NOMOVE
                                                                           | Win32.SetWindowPosFlags.SWP_NOZORDER
                                                                           | Win32.SetWindowPosFlags.SWP_NOACTIVATE);
            }

            public void Begin()
            {
                m_PreviousHDC = Win32.wglGetCurrentDC();
                m_PreviousHglrc = Win32.wglGetCurrentContext();

                if (m_PreviousHDC != m_HDC || m_PreviousHglrc != m_Hglrc)
                    Win32.wglMakeCurrent(m_HDC, m_Hglrc);
            }

            public void End()
            {
                if (m_PreviousHDC != m_HDC || m_PreviousHglrc != m_Hglrc)
                    Win32.wglMakeCurrent(m_PreviousHDC, m_PreviousHglrc);
            }

            public void SwapBuffers()
            {
                Win32.SwapBuffers(m_HDC);
            }

            public void Dispose()
            {
                Win32.wglDeleteContext(m_Hglrc);
            }
        }
    }
}
