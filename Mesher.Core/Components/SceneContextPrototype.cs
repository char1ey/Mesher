using System;
using System.Drawing;
using System.Windows.Forms;
using Mesher.Core.Objects.Camera;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;
using Mesher.Mathematics;

namespace Mesher.Core.Components
{
    public partial class SceneContextPrototype : UserControl
    {
        private readonly RenderContext m_renderContext;

        private MouseButtons m_previousMouseButton;

        public Camera Camera { get; set; }

        public CameraControler CameraControler;

        public RenderContext RenderContext
        {
            get { return m_renderContext; }
        }

        public SceneContextPrototype(DataContext dataContext)
        {
            m_renderContext = dataContext.CreateRenderWindow(Handle);
            m_renderContext.ClearColor = Color.DimGray;
            InitializeComponent();
        }

        public void BeginRender()
        {
            m_renderContext.Begin();
            m_renderContext.Clear();
        }

        public void EndRender()
        {
            m_renderContext.End();
            m_renderContext.SwapBuffers();
        }

        public void Render(Scene scene, RendererBase renderer)
        {
            if (Camera == null)
                Camera = new OrthographicCamera(Width, Height, new Vec3(0, 0, 1), new Vec3(0, 1, 0), new Vec3(0, 0, 0));

            renderer.Render(scene, Camera);
        }

        protected override void OnResize(EventArgs e)
        {
            m_renderContext.ResizeWindow(Width, Height);

            if (Camera != null)
                ((OrthographicCamera)Camera).UpdateSize(Width, Height);

            base.OnResize(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            var delta = (Single)e.Delta / SystemInformation.MouseWheelScrollDelta;
            CameraControler.Zoom(e.Location, delta);
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
                CameraControler.Inaction(e.Location);
            else
            {
                if (e.Button == MouseButtons.Left)
                    CameraControler.Move(e.Location);
                else if (e.Button == MouseButtons.Right)
                    CameraControler.Rotate(e.Location);
            }
            m_previousMouseButton = e.Button;

            base.OnMouseMove(e);
        }
    }
}
