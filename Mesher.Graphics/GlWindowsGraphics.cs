using System;
using Mesher.Graphics.Data;
using Mesher.Graphics.Data.OpenGL;
using Mesher.Graphics.RenderContexts;
using Mesher.Graphics.Renderers;
using Mesher.Graphics.Renderers.OpenGL;

namespace Mesher.Graphics
{
    public class GlWindowsGraphics : MesherGraphics
    {
        private GlDataContext m_dataContext;

        public override IDataContext DataContext
        {
            get { return m_dataContext; }
        }

        public GlWindowsGraphics()
        {
        }

        public override IRenderContext CreateRenderContext(IntPtr handle)
        {
            var renderContext = new GlWindowsRenderContext(handle);

            if (m_dataContext == null)
                m_dataContext = new GlDataContext(renderContext);

            renderContext.DataContext = m_dataContext;

            return renderContext;
        }

        protected override RenderersFactory CreateRSceneRenderer()
        {
            return new DefaultGlRenderersFactory();
        }
    }
}
