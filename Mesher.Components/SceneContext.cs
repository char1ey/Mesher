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
using Mesher.GraphicsCore.Objects;

namespace Mesher.Components
{
    public partial class SceneContext : UserControl
    {
        private const Double Eps = 1e-9;
        private const Double ZoomSpeed = 1.2;
        private const Double RotationSpeed = 5;

        private readonly RenderWindow m_renderWindow;

        private MouseButtons m_previousMouseButton;
        private Vec2 m_previousMousePosition;

        public Camera Camera { get; set; }

        public SceneContext(RenderManager renderManager)
        {
            m_renderWindow = renderManager.CreateRenderWindow(Handle);
            m_renderWindow.ClearColor = Color.DimGray;
            InitializeComponent();
        }

        public void BeginRender()
        {
            m_renderWindow.Begin();
            m_renderWindow.Clear();
        }

        public void EndRender()
        {
            m_renderWindow.End();
            m_renderWindow.SwapBuffers();
        }

        public void Render(Scene scene)
        {
            if (Camera == null)
            {
                Camera = scene.Cameras.Add<OrthographicCamera>();
                Camera.Position = new Vec3(0, 0, 1);
                Camera.UpVector = new Vec3(0, 1, 0);
                Camera.LookAtPoint = new Vec3(0, 0, 0);
                Camera.ProjectionMatrix = Mat4.Ortho(-0.02 * Width, 0.02 * Width, -0.02 * Height, 0.02 * Height, -1000000, 1000000);
                Camera.Apply();
            }

            m_renderWindow.Render(scene, Camera.Id);
        }
        protected override void OnResize(EventArgs e)
        {
            m_renderWindow.ResizeWindow(Width, Height);

            if (Camera != null)
            {
                Camera.ProjectionMatrix = Mat4.Ortho(-0.02 * Width, 0.02 * Width, -0.02 * Height, 0.02 * Height, -1000000, 1000000);
                Camera.Apply();
            }

            base.OnResize(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            var zoom = (Double) e.Delta / SystemInformation.MouseWheelScrollDelta * ZoomSpeed;
            Camera.Zoom(zoom < 0 ? -1 / zoom : zoom);

            var viewport = new Vec4(0, 0, Width, Height);

            var a = Camera.UnProject(Width / 2f, Height / 2f, viewport);
            var b = Camera.UnProject(e.X, Height - e.Y, viewport);
            
            a = Plane.XYPlane.Cross(new Line(a, (Camera.LookAtPoint - Camera.Position).Normalize()));
            b = Plane.XYPlane.Cross(new Line(b, (Camera.LookAtPoint - Camera.Position).Normalize()));

            Camera.Move((b - a) * zoom / 7.2);
  
            Camera.Apply();
            base.OnMouseWheel(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LShiftKey)
                m_previousMouseButton = MouseButtons.None;
            base.OnKeyUp(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LShiftKey)
                m_previousMouseButton = MouseButtons.None;
            base.OnKeyDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Camera == null)
                return;

            if (e.Button == MouseButtons.None || m_previousMouseButton != e.Button)
            {
                Camera.ClearStack();
                m_previousMousePosition = new Vec2(e.X, e.Y);
            }
            else
            {
                Camera.Pop();

                if (e.Button == MouseButtons.Left)
                {
                    var viewport = new Vec4(0, 0, Width, Height);
                    var a = Camera.UnProject(m_previousMousePosition.X, Height - m_previousMousePosition.Y, viewport);
                    var b = Camera.UnProject(e.X, Height - e.Y, viewport);

                    Camera.Push();
                    Camera.Move(a - b);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Camera.Push();

                    var v0 = GetArcBallVector((Int32)m_previousMousePosition.X, (Int32)m_previousMousePosition.Y);
                    var v1 = GetArcBallVector(e.X, e.Y);

                    var axis = Mat3.Inverse(Camera.ViewMatrix.ToMat3()) * (Camera.ProjectionMatrix.ToMat3() * v0.Cross(v1).Normalize()).Normalize();
                    var angle = v0.Angle(v1);

                    Camera.Rotate(axis, -angle * RotationSpeed);
                }
            }
            m_previousMouseButton = e.Button;

            Camera.Apply();
            base.OnMouseMove(e);
        }

        private Vec3 GetArcBallVector(Int32 x, Int32 y)
        {
            var ret = new Vec3(2f * x / Width - 1, 2f * y / Height - 1, 0);

            ret.Y = -ret.Y;

            var opSqr = ret.X * ret.X + ret.Y * ret.Y;

            if (opSqr <= 1)
                ret.Z = Math.Sqrt(1 - opSqr);
            else ret = ret.Normalize();

            return ret;
        }
    }
}
