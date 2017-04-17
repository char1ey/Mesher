using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Mesher.Mathematics;
using static Mesher.GraphicsCore.Win32;

namespace Mesher.GraphicsCore
{

    public class RenderContext : NativeWindow, IDisposable
    {
        internal IntPtr HDC;
        internal IntPtr Hglrc;

        internal Control ContextControl;

        public RenderContext(IntPtr handle)
        {
            var createParams = new CreateParams
            {
                Parent = handle,
                Style = (int)(WindowStyles.WS_CHILD | WindowStyles.WS_VISIBLE | WindowStyles.WS_DISABLED)
            };
            
            CreateHandle(createParams);

            HDC = GetDC(Handle);

            if(HDC != null)
            {
                var pfd = new PIXELFORMATDESCRIPTOR()
                {
                    nSize = (ushort)Marshal.SizeOf<PIXELFORMATDESCRIPTOR>(),
                    nVersion = 1,
                    dwFlags = PFD_DRAW_TO_WINDOW | PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER,
                    iPixelType = PFD_TYPE_RGBA,
                    cColorBits = 32,
                    cDepthBits = 24,
                    cStencilBits = 8,
                    iLayerType = PFD_MAIN_PLANE
                };
                int pixelFrormat = ChoosePixelFormat(HDC, pfd);
                SetPixelFormat(HDC, pixelFrormat, pfd);
                Hglrc = wglCreateContext(HDC);
                wglMakeCurrent(HDC, Hglrc);
            }

            ContextControl = Control.FromChildHandle(Handle);
            ContextControl.MouseMove += M_control_MouseMove;
            ContextControl.MouseUp += M_control_MouseUp;
            ContextControl.MouseWheel += M_control_MouseWheel;
            Init();
        }

        private void M_control_MouseWheel(object sender, MouseEventArgs e)
        {
          //  camera.Zoom(e.Delta/6.0, new Vec2(e.X, e.Y));
            Render();
        }

        private void M_control_MouseUp(object sender, MouseEventArgs e)
        {
            prevKeyPressed = MouseButtons.None;
            camera.Fix();
           // camera.ClearPosition();
        }
  
        private MouseButtons prevKeyPressed;
        private Vertex prevMousePosition = new Vertex(float.NegativeInfinity, -1, 0);
        
        private void M_control_MouseMove(object sender, MouseEventArgs e)
        {
            Render();

            if(prevMousePosition.x == float.NegativeInfinity || !prevKeyPressed.Equals(e.Button))
            {
                prevMousePosition = new Vertex(e.X, e.Y, 0);
                prevKeyPressed = e.Button;
                return;
            }

            var p1 = gl.UnProject(prevMousePosition.x, ContextControl.Height - prevMousePosition.y, 0);
            var p2 = gl.UnProject(e.X, ContextControl.Height - e.Y, 0);

           if(e.Button == MouseButtons.Left)
            {
                //camera.RevertPosition();
                camera.Move(p1 - p2, true);
            }
            else if(e.Button == MouseButtons.Right)
            {
               // camera.RevertPosition();
                camera.Rotate((p2 - camera.Center).Cross(p1 - camera.Center).Normalize(), p1.Normalize().Angle(p2.Normalize()), true);
            }
        }


        uint[] VBO;

        private void Init()
        {
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);

            var vertices_data = new float[] {
                -1.0f, -1.0f, 0.0f,
                1.0f, -1.0f, 0.0f,
                0.0f, 1.0f, 0.0f
            };
            
            VBO = new uint[1];

            gl.GenBuffers(1, VBO);
            gl.BindBuffer(gl.GL_ARRAY_BUFFER, VBO[0]);
            gl.BufferData(gl.GL_ARRAY_BUFFER, vertices_data, gl.GL_STATIC_DRAW);
        }

        Camera camera;

        public void ResizeContext(int width, int height)
        {
            gl.Viewport(0, 0, width, height);
            camera = new Camera();
            SetWindowPos(Handle, IntPtr.Zero, 0, 0, width, height, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_NOACTIVATE);
        }

        float angle = 0.0f;
        public void Render()
        {
            camera.ApplyCamera(this);

            var pHDC = wglGetCurrentDC();
            var pRC = wglGetCurrentContext();

            if(pHDC != HDC || pRC != Hglrc)
                wglMakeCurrent(HDC, Hglrc);

            gl.Clear(gl.GL_COLOR_BUFFER_BIT | gl.GL_DEPTH_BUFFER_BIT);

            gl.MatrixMode(gl.GL_MODELVIEW);
            gl.PushMatrix();
            gl.Rotate(0, 0, angle);
            gl.EnableVertexAttribArray(0);
            gl.BindBuffer(gl.GL_ARRAY_BUFFER, VBO[0]);
            gl.VertexAttribPointer(0, 3, gl.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.Color(1.0, 1.0, 1.0);
            gl.DrawArrays(gl.GL_TRIANGLES, 0, 3);
            gl.PopMatrix();
            gl.DisableVertexAttribArray(0);

            for (float i = -50; i <= 50; i += 5)
            {
                gl.Begin(gl.GL_LINES);
                gl.Color(0.0, 1.0, 0.0);
                gl.Vertex(-50, 0, i);
                gl.Vertex(50, 0, i);
                gl.Vertex(i, 0, -50);
                gl.Vertex(i, 0, 50);
                gl.End();
            }

            gl.Flush();

           /* angle += 10.0f;
            if (angle > 360.0f)
                angle -= 360;*/

            SwapBuffers(HDC);

            if (pHDC != HDC || pRC != Hglrc)
                wglMakeCurrent(pHDC, pRC);
        }

        public void Dispose()
        {
            wglDeleteContext(Hglrc);
        }
    }
}
