using System;
using Mesher.Core.Collections;
using Mesher.Core.Components;
using Mesher.Core.Objects.Camera;
using Mesher.Core.Objects.Scene;
using Mesher.Core.Renderers;
using Mesher.GraphicsCore;
using Mesher.Mathematics;

namespace Mesher.Core.SceneForm
{
    public class SceneForm
    {
        private SceneContextPrototype m_renderContext;
        private RendererBase m_renderer;

        public Scene Scene { get; set; }
        public Camera Camera
        {
            get { return m_renderContext.Camera; }
        }

        public SceneFormComponents Components { get; private set; }

        public Int32 Width
        {
            get { return m_renderContext.Width; }
        }
        public Int32 Height
        {
            get { return m_renderContext.Height; }
        }

        public SceneForm(SceneContextPrototype renderContext, RendererBase renderer)
        {
            m_renderer = renderer;
            m_renderContext = renderContext;
            Components = new SceneFormComponents();
            Components.Add(new AxisComponent(this));
        }

        public void Render()
        {
            if (m_renderContext.Camera == null)
                m_renderContext.Camera = new OrthographicCamera(m_renderContext.Width, m_renderContext.Height, new Vec3(0, 0, 1), new Vec3(0, 1, 0), new Vec3(0, 0, 0));

            BeginRender();
            m_renderer.Render(Scene, m_renderContext.Camera);
         /*   EndRender();

            BeginRender();*/
            Gl.Clear(Gl.GL_DEPTH_BUFFER_BIT);
            foreach(var component in Components)
                component.Draw();
            EndRender();
        }

        public void BeginRender()
        {
            m_renderContext.BeginRender();
        }

        public void EndRender()
        {
            m_renderContext.EndRender();
        }
    }
}
