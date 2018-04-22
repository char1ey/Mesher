using System;
using System.Drawing;
using System.Windows.Forms;
using Mesher.Core.Collections;
using Mesher.Core.Objects.Camera;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Renderers;
using Mesher.Core.SceneContexts.Components;
using Mesher.GraphicsCore;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.Mathematics;

namespace Mesher.Core.SceneContexts
{
    public partial class SceneContextWinforms : UserControl, ISceneContext
    {
        private readonly WindowsRenderContext m_renderContext;

        private SceneContextGraphics m_sceneContextGraphics;

        private MouseButtons m_previousMouseButton;

        public Camera Camera
        {
            get { return RenderContext.Camera; }
            set { RenderContext.Camera = value; }
        }
        public Scene Scene { get; set; }
        public SceneRendererBase SceneRenderer { get; set; }
        public CameraControler CameraControler { get; set; }
        public WindowsRenderContext RenderContext
        {
            get { return m_renderContext; }
        }

        public DataContext DataContext
        {
            get { return m_renderContext?.DataContext; }
        }

        public SceneFormComponents SceneContextComponents { get; private set; }


        public void Add(SceneContextComponent component)
        {
            SceneContextComponents.Add(component);
        }

        public void Remove(SceneContextComponent component)
        {
            SceneContextComponents.Remove(component);
        }

        public void RemoveAt(Int32 id)
        {
            SceneContextComponents.RemoveAt(id);
        }

        public SceneContextWinforms(DataContext dataContext)
        {
            m_renderContext = dataContext.CreateRenderWindow(Handle);
            m_renderContext.ClearColor = Color.DimGray;
            InitializeComponent();
            SceneContextComponents = new SceneFormComponents();
            m_sceneContextGraphics = new SceneContextGraphics(this);
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

        public void Render()
        {
            if (Camera == null)
                Camera = new OrthographicCamera(m_renderContext.Width, m_renderContext.Height, new Vec3(0, 0, 1), new Vec3(0, 1, 0), new Vec3(0, 0, 0));

            BeginRender();
            SceneRenderer.Render(Scene, Camera);
     
            Gl.Clear(Gl.GL_DEPTH_BUFFER_BIT);
            foreach (var component in SceneContextComponents)
                component.Draw(m_sceneContextGraphics);
            EndRender();
        }

        protected override void OnResize(EventArgs e)
        {
            m_renderContext.SetSize(Width, Height);

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

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
                foreach (var component in SceneContextComponents)
                    component.MouseClick(new Point(e.Location.X, Height - e.Location.Y));
            base.OnMouseClick(e);
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

            foreach (var component in SceneContextComponents)
                component.MouseMove(new Point(e.Location.X, Height - e.Location.Y));

            base.OnMouseMove(e);
        }
    }
}
