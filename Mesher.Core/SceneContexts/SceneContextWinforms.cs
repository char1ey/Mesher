using System;
using System.Drawing;
using System.Windows.Forms;
using Mesher.Core.Collections;
using Mesher.Core.Objects.Camera;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Renderers;
using Mesher.Core.SceneForm;
using Mesher.GraphicsCore;
using Mesher.Mathematics;

namespace Mesher.Core.SceneContexts
{
    public partial class SceneContextWinforms : UserControl, ISceneContext
    {
        private readonly RenderContext m_renderContext;

        private MouseButtons m_previousMouseButton;

        public Camera Camera { get; set; }
        public Scene Scene { get; set; }
        public Renderer Renderer { get; set; }
        public CameraControler CameraControler { get; set; }
        public SceneFormComponents SceneContextComponents { get; private set; }


        public void Add(SceneContextComponent component)
        {
            throw new NotImplementedException();
        }

        public void Remove(SceneContextComponent component)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(Int32 id)
        {
            throw new NotImplementedException();
        }

        public SceneContextWinforms(DataContext dataContext)
        {
            m_renderContext = dataContext.CreateRenderWindow(Handle);
            m_renderContext.ClearColor = Color.DimGray;
            InitializeComponent();
            SceneContextComponents = new SceneFormComponents();
            SceneContextComponents.Add(new Axises(this));
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

        public void Render()
        {
            if (Camera == null)
                Camera = new OrthographicCamera(m_renderContext.Width, m_renderContext.Height, new Vec3(0, 0, 1), new Vec3(0, 1, 0), new Vec3(0, 0, 0));

            BeginRender();
            Renderer.Render(Scene, Camera);
     
            Gl.Clear(Gl.GL_DEPTH_BUFFER_BIT);
            foreach (var component in SceneContextComponents)
                component.Draw();
            EndRender();
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

            foreach (var component in SceneContextComponents)
                component.MouseMove(e.Location);

            base.OnMouseMove(e);
        }
    }
}
