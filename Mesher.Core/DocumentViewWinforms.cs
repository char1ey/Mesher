using System;
using System.Drawing;
using System.Windows.Forms;
using Mesher.Core.Collections;
using Mesher.Core.Events;
using Mesher.Core.Events.EventArgs;
using Mesher.Core.Objects;
using Mesher.Graphics.Camera;
using Mesher.Graphics.RenderContexts;
using Mesher.Mathematics;

namespace Mesher.Core
{
    public partial class DocumentViewWinforms : UserControl, IDocumentView
    {
        private readonly GlWindowsRenderContext m_renderContext;

        private MouseButtons m_previousMouseButton;

        private MesherApplication m_mesherApplication;

        public RCamera Camera
        {
            get
            {
                if (m_renderContext.RCamera == null)
                    m_renderContext.RCamera = new OrthographicRCamera(m_renderContext.Width, m_renderContext.Height, new Vec3(0, 0, 1), new Vec3(0, 1, 0), new Vec3(0, 0, 0));
                return m_renderContext.RCamera;
            }
            set { m_renderContext.RCamera = value; }
        }

        public Document Document { get; set; }
        public CameraControler CameraControler { get; set; }

        public IRenderContext RenderContext
        {
            get { return m_renderContext; }
        }

        public event OnAfterDocumentViewRender AfterDocumentViewRender;

        public DocumentViewWinforms(MesherApplication application)
        {
            m_mesherApplication = application;
            m_renderContext = (GlWindowsRenderContext)application.Graphics.CreateRenderContext(Handle);
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

        public void Render(Scene scene)
        {
            scene.Render(this);
 
            var args = new DocumentViewRenderEventArgs(m_mesherApplication.Graphics);
            AfterDocumentViewRender?.Invoke(this, args);

            foreach(var postRenderItem in args.PostRenderItems)
                foreach(var primitive in postRenderItem.Primitives)
                    primitive.Render(m_mesherApplication.Graphics.RenderersFactory, postRenderItem.RenderArgs);

            foreach (var postRenderItem in args.PostRenderItems)
            {
                postRenderItem.RenderArgs.RCamera.Dispose();
                postRenderItem.RenderArgs.RLights.Dispose();

                foreach(var primitive in postRenderItem.Primitives)
                    primitive.Dispose();
            }
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
