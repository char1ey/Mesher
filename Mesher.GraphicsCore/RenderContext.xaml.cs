using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Mesher.GraphicsCore
{
    /// <summary>
    /// Логика взаимодействия для RenderContext.xaml
    /// </summary>
    public partial class RenderContext : IDisposable
    {
        private readonly GlWindow m_glWindow;

        public event EventHandler ContextResize;

        public new event MouseEventHandler MouseMove;
        public new event MouseEventHandler MouseWheel;

        public new event System.Windows.Input.KeyEventHandler KeyUp;
        public new event System.Windows.Input.KeyEventHandler KeyDown;

        public Color ClearColor { get; set; }

        public RenderContext(Color color)
        {
            InitializeComponent();

            WindowsFormsHost.Child = new Control();

            m_glWindow = new GlWindow(WindowsFormsHost.Child.Handle);

            WindowsFormsHost.Child.MouseWheel += Child_MouseWheel;
            WindowsFormsHost.Child.MouseMove += Child_MouseMove;

            WindowsFormsHost.KeyDown += WindowsFormsHost_KeyDown;
            WindowsFormsHost.KeyUp += WindowsFormsHost_KeyUp;

            WindowsFormsHost.Child.Resize += Child_Resize;

            m_glWindow.Begin();
            Gl.ClearColor(color.R / 256f, color.G / 256f, color.B / 256f, 1.0f);
            m_glWindow.End();
        }

        public RenderContext() : this(Color.DimGray) { }

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
            Width = control.Width;
            Height = control.Height;
            m_glWindow.ResizeWindow(control.Width, control.Height);
            ContextResize?.Invoke(sender, e);
        }

        public void BeginRender(bool clear = false)
        {
            m_glWindow.Begin();
            if (clear)
                Gl.Clear(Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_COLOR_BUFFER_BIT);
        }

        public void EndRender(bool swapBuffers = false)
        {
            if (swapBuffers)
                m_glWindow.SwapBuffers();
            m_glWindow.End();
        }

        public void Dispose()
        {
            m_glWindow.Dispose();
        }

        sealed class GlWindow : NativeWindow, IDisposable
        {
            private readonly IntPtr m_hdc;
            private readonly IntPtr m_hglrc;

            private IntPtr m_previousHdc;
            private IntPtr m_previousHglrc;

            public GlWindow(IntPtr handle)
            {
                var createParams = new CreateParams
                {
                    Parent = handle,
                    Style = (int)(Win32.WindowStyles.WS_CHILD | Win32.WindowStyles.WS_VISIBLE | Win32.WindowStyles.WS_DISABLED)
                };

                CreateHandle(createParams);

                m_hdc = Win32.GetDC(Handle);

                if (m_hdc == null) return;

                var pfd = new Win32.PIXELFORMATDESCRIPTOR
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
                var pixelFrormat = Win32.ChoosePixelFormat(m_hdc, pfd);
                Win32.SetPixelFormat(m_hdc, pixelFrormat, pfd);
                m_hglrc = Win32.wglCreateContext(m_hdc);
                Win32.wglMakeCurrent(m_hdc, m_hglrc);
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
                m_previousHdc = Win32.wglGetCurrentDC();
                m_previousHglrc = Win32.wglGetCurrentContext();

                if (m_previousHdc != m_hdc || m_previousHglrc != m_hglrc)
                    Win32.wglMakeCurrent(m_hdc, m_hglrc);
            }

            public void End()
            {
                if (m_previousHdc != m_hdc || m_previousHglrc != m_hglrc)
                    Win32.wglMakeCurrent(m_previousHdc, m_previousHglrc);
            }

            public void SwapBuffers()
            {
                Win32.SwapBuffers(m_hdc);
            }

            public void Dispose()
            {
                Win32.wglDeleteContext(m_hglrc);
            }
        }
    }
}
