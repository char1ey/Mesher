using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Mesher.Mathematics;
using System.Drawing;

namespace Mesher.GraphicsCore
{
    public sealed class RenderContext : NativeWindow, IDisposable
    {
        private IntPtr m_hdc;
        private IntPtr m_hglrc;

        private IntPtr m_previousHdc;
        private IntPtr m_previousHglrc;

        public IntPtr GlrcHandle
        {
            get { return m_hglrc; }
        }

        public Color ClearColor { get; set; }

        private void CreateDeviceContext(IntPtr handle)
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

        }

        public RenderContext(IntPtr handle)
        {
            CreateDeviceContext(handle);
            m_hglrc = Win32.wglCreateContext(m_hdc);
            Win32.wglMakeCurrent(m_hdc, m_hglrc);
        }

        public RenderContext(IntPtr handle, IntPtr hglrc)
        {
            CreateDeviceContext(handle);
            m_hglrc = hglrc;
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

        public Vec3 UnProject(Vec2 v)
        {
            return UnProject(v.X, v.Y);
        }

        public Vec3 UnProject(double x, double y)
        {
            Begin();
            var ret = Gl.UnProject(x, y, 0);
            End();
            return ret;
        }

        public Vec2 Project(double x, double y, double z)
        {
            Begin();
            var ret = Gl.Project(x, y, z);
            End();
            return ret;
        }

        public Vec2 Project(Vec3 v)
        {
            return Project(v.X, v.Y, v.Z);
        }

        public void Begin()
        {
            m_previousHdc = Win32.wglGetCurrentDC();
            m_previousHglrc = Win32.wglGetCurrentContext();

            if (m_previousHdc != m_hdc || m_previousHglrc != m_hglrc)
                Win32.wglMakeCurrent(m_hdc, m_hglrc);
        }

        public void Clear()
        {
            Begin();
            Gl.ClearColor(ClearColor);
            Gl.Clear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.Enable(Gl.GL_DEPTH_TEST);
            End();
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
