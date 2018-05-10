using System;
using System.Drawing;
using System.Windows.Forms;
using Mesher.Core.Collections;
using Mesher.Core.Objects;
using Mesher.Core.SceneContexts.Components;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.Collections;
using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;
using Mesher.Mathematics;

namespace Mesher.Core
{
    public partial class SceneContextWinforms : UserControl, ISceneContext
    {
        private readonly IRenderContext m_renderContext;

        private MouseButtons m_previousMouseButton;

        public RCamera Camera
        {
            get { return RenderContext.RCamera; }
            set { RenderContext.RCamera = value; }
        }
        public Scene Scene { get; set; }
        public RenderersFactory RenderersFactory { get; set; }
        public CameraControler CameraControler { get; set; }
        public IRenderContext RenderContext
        {
            get { return m_renderContext; }
            set { }
        }

        public SceneContextWinforms()
        {
            m_renderContext = new GlWindowsRenderContext(Handle);
            m_renderContext.ClearColor = Color.DimGray;
            InitializeComponent();
        }

        public void BeginRender()
        {
            m_renderContext.BeginRender();
            m_renderContext.ClearColorBuffer(Color.DimGray);
            m_renderContext.ClearDepthBuffer();
        }

        public void EndRender()
        {
            m_renderContext.EndRender();
            m_renderContext.SwapBuffers();
        }

        public void Render(RPrimitive primitive)
        {
            if (Camera == null)
                Camera = new OrthographicRCamera(m_renderContext.Width, m_renderContext.Height, new Vec3(0, 0, 1), new Vec3(0, 1, 0), new Vec3(0, 0, 0));

            BeginRender();

            primitive.Render(RenderersFactory, new RLights(), m_renderContext);

            EndRender();
        }

        protected override void OnResize(EventArgs e)
        {
            m_renderContext.SetSize(Width, Height);

            if (Camera != null)
                ((OrthographicRCamera)Camera).UpdateSize(Width, Height);

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
