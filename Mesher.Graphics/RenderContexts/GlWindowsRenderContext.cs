using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Mesher.Graphics.Data.OpenGL;
using Mesher.Graphics.Imports;

namespace Mesher.Graphics.RenderContexts
{
    public sealed class GlWindowsRenderContext : IRenderContext, IDisposable
    {
        private IntPtr m_hdc;
        private IntPtr m_handle;

        private IntPtr m_previousHdc;
        private IntPtr m_previousHglrc;

        internal IntPtr RenderWindowHandle { get { return m_hdc; } }

        public Int32 Width { get; private set; }
        public Int32 Height { get; private set; }
        public Camera.RCamera RCamera { get; set; }

        public Color ClearColor { get; set; }

        public GlDataContext DataContext { get; set; }

        internal GlWindowsRenderContext(IntPtr handle)
        {
            m_handle = handle;

            m_hdc = Win32.GetDC(handle);

            if (m_hdc == null) return;

            var pfd = new Win32.PIXELFORMATDESCRIPTOR
            {
                nSize = (UInt16)Marshal.SizeOf(new Win32.PIXELFORMATDESCRIPTOR()),
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

        public void SetSize(Int32 width, Int32 height)
        {
            Width = width;
            Height = height;
            //TODO move viewport to camera
            Win32.glViewport(0, 0, width, height);
        }

        public void BeginRender()
        {
            if (DataContext == null)
                return;
            Win32.wglMakeCurrent(RenderWindowHandle, DataContext.GlrcHandle);
        }

        public void EndRender()
        {
        }

        public void ClearColorBuffer(Color color)
        {
            Gl.ClearColor(color);
            Gl.Enable(Gl.GL_DEPTH_TEST);
            Gl.Clear(Gl.GL_COLOR_BUFFER_BIT);
        }

        public void ClearDepthBuffer()
        {
            Gl.Enable(Gl.GL_DEPTH_TEST);
            Gl.Clear(Gl.GL_DEPTH_BUFFER_BIT);
        }

        public void SwapBuffers()
        {
            Win32.SwapBuffers(m_hdc);
        }

        public void Dispose()
        {
            Win32.ReleaseDC(m_handle, m_hdc);
        }
    }
}
