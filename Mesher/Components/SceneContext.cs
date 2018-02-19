using System;
using System.Drawing;
using System.Windows.Forms;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.Objects;
using Mesher.Mathematics;

namespace Mesher.Core.Components
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
                Camera = new OrthographicCamera(Width, Height, new Vec3(0, 0, 1), new Vec3(0, 1, 0), new Vec3(0, 0, 0));
                scene.Cameras.Add((OrthographicCamera)Camera);
            }

            m_renderWindow.Render(scene, Camera.Id);
        }
        protected override void OnResize(EventArgs e)
        {
            m_renderWindow.ResizeWindow(Width, Height);

            if (Camera != null)
            {
                ((OrthographicCamera)Camera).UpdateSize(Width, Height);
            }

            base.OnResize(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            var zoom = (Double) e.Delta / SystemInformation.MouseWheelScrollDelta * ZoomSpeed;
            Camera.Zoom(zoom < 0 ? -1 / zoom : zoom);

            var a = Camera.UnProject(Width / 2f, Height / 2f, 0, Width, Height);
            var b = Camera.UnProject(e.X, Height - e.Y, 0, Width, Height);
            
            a = Plane.XYPlane.Cross(new Line(a, (Camera.LookAtPoint - Camera.Position).Normalize()));
            b = Plane.XYPlane.Cross(new Line(b, (Camera.LookAtPoint - Camera.Position).Normalize()));

            Camera.Move((b - a) * zoom / 7.2);

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
                    var a = Camera.UnProject(m_previousMousePosition.X, Height - m_previousMousePosition.Y, 0, Width, Height);
                    var b = Camera.UnProject(e.X, Height - e.Y, 0, Width, Height);

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
