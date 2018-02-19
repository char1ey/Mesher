using Mesher.GraphicsCore.Objects;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Mesher.Mathematics;

namespace Mesher.GraphicsCore
{
    public sealed class RenderWindow : NativeWindow, IDisposable
    {
        private IntPtr m_hdc;

        private IntPtr m_previousHdc;
        private IntPtr m_previousHglrc;

        internal IntPtr RenderWindowHandle { get { return m_hdc; } }

        private Int32 m_beginModeDepth;

        public Int32 Width { get; private set; }
        public Int32 Height { get; private set; }

        public Color ClearColor { get; set; }

        public RenderManager RenderManager { get; internal set; }

        internal RenderWindow(IntPtr handle)
        {
            var createParams = new CreateParams
            {
                Parent = handle,
                Style = (Int32)(Win32.WindowStyles.WS_CHILD | Win32.WindowStyles.WS_VISIBLE | Win32.WindowStyles.WS_DISABLED)
            };

            CreateHandle(createParams);

            m_hdc = Win32.GetDC(Handle);

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

            m_beginModeDepth = 0;
        }

        public void ResizeWindow(Int32 width, Int32 height)
        {
            Width = width;
            Height = height;
            Win32.glViewport(0, 0, width, height);
            Win32.SetWindowPos(Handle, IntPtr.Zero, 0, 0, width, height, Win32.SetWindowPosFlags.SWP_NOMOVE
                                                                       | Win32.SetWindowPosFlags.SWP_NOZORDER
                                                                       | Win32.SetWindowPosFlags.SWP_NOACTIVATE);
        }

        public void Begin()
        {
            m_beginModeDepth++;
            if (m_beginModeDepth != 1)
                return;

            m_previousHdc = Win32.wglGetCurrentDC();
            m_previousHglrc = Win32.wglGetCurrentContext();

            if (m_previousHdc != RenderWindowHandle || m_previousHglrc != RenderManager.GlrcHandle)
                Win32.wglMakeCurrent(RenderWindowHandle, RenderManager.GlrcHandle);
        }

        public void End()
        {
            m_beginModeDepth--;

            if (m_beginModeDepth != 0)
                return;

            if (m_previousHdc != RenderWindowHandle || m_previousHglrc != RenderManager.GlrcHandle)
                Win32.wglMakeCurrent(m_previousHdc, m_previousHglrc);
        }

        public void Clear()
        {
            Begin();
            Gl.ClearColor(ClearColor);
            Gl.Clear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.Enable(Gl.GL_DEPTH_TEST);
            End();
        }
        public void SwapBuffers()
        {
            Win32.SwapBuffers(m_hdc);
        }

        public void Render(Scene scene, Int32 cameraId)
        {
            Begin();
            RenderManager.ShaderProgram.Render(scene, cameraId);
            End();
        }

        public void Dispose()
        {
            Win32.ReleaseDC(Handle, m_hdc);
        }
    }
}
