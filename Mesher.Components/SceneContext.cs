using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Camera;
using Mesher.Mathematics;

namespace Mesher.Components
{
    public partial class SceneContext : UserControl
    {
        private readonly RenderContext m_renderContext;

        private bool m_isShiftPressed;
        private MouseButtons m_previousMouseButton;
        private Vec2 m_previousMousePosition;

        private Vec3 m_previousCameraPosition;
        private Vec3 m_previousCameraUpVector;
        private Vec3 m_previousCameraLookAtPoint;

        public Camera Camera { get; set; }

        public SceneContext()
        {
            m_renderContext = new RenderContext(Handle);
            InitializeComponent();
        }

        public void BeginRender()
        {
            m_renderContext.Begin();
        }

        public void EndRender()
        {
            m_renderContext.End();
            m_renderContext.SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            m_renderContext.ResizeWindow(Width, Height);
            if(Camera == null)
                Camera = new OrthographicCamera(Width * 0.04, Height * 0.04, new Vec3(0, 0, 1), new Vec3(0, 1, 0), new Vec3(0, 0, 0));
            Camera.ProjectionMatrix = Mat4.Ortho(-Width * 0.02, Width * 0.02, -Height * 0.02, Height * 0.02, -1000000, 1000000);
            base.OnResize(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            var zoom = (double) e.Delta / SystemInformation.MouseWheelScrollDelta * 1.2;
            if (zoom < 0) zoom = -zoom;
            else zoom = 1 / zoom;
            Camera.Zoom(zoom);
            Camera.Apply();
            base.OnMouseWheel(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LShiftKey)
            {
                m_isShiftPressed = false;
                m_previousMouseButton = MouseButtons.None;
            }

            base.OnKeyUp(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LShiftKey)
            {
                m_isShiftPressed = true;
                m_previousMouseButton = MouseButtons.None;
            }
            base.OnKeyDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Camera == null)
                return;

            if (e.Button == MouseButtons.None || m_previousMouseButton != e.Button)
            {
                m_previousMousePosition = new Vec2(e.X, e.Y);
                m_previousCameraLookAtPoint = Camera.LookAtPoint;
                m_previousCameraUpVector = Camera.UpVector;
                m_previousCameraPosition = Camera.Position;
            }
            else
            {
                Camera.LookAtPoint = m_previousCameraLookAtPoint;
                Camera.UpVector = m_previousCameraUpVector;
                Camera.Position = m_previousCameraPosition;

                if (e.Button == MouseButtons.Left)
                {
                    Camera.Move(m_renderContext.UnProject(m_previousMousePosition.X, Height - m_previousMousePosition.Y)
                                - m_renderContext.UnProject(e.X, Height - e.Y));
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Camera.Rotate(
                        (m_previousCameraPosition - m_previousCameraLookAtPoint).Cross(m_previousCameraUpVector),
                        (e.Y - m_previousMousePosition.Y) / Height * Math.PI * 2);
                    Camera.Rotate(new Vec3(0, 0, 1), (m_previousMousePosition.X - e.X) / Width * Math.PI * 2);
                }
            }
            m_previousMouseButton = e.Button;

            Camera.Apply();
            base.OnMouseMove(e);
        }
    }
}
