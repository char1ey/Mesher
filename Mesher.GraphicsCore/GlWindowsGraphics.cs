using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.Data.OpenGL;
using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;
using Mesher.GraphicsCore.Renderers.OpenGL;

namespace Mesher.GraphicsCore
{
    public class GlWindowsGraphics : MesherGraphics
    {
        private GlDataContext m_dataContext;

        public override IDataContext DataContext
        {
            get { return m_dataContext; }
        }

        public GlWindowsGraphics(GlWindowsRenderContext renderContext)
        {
            var dataContext = new GlDataContext(renderContext);
            renderContext.DataContext = dataContext;
            m_dataContext = dataContext;
        }

        public override IRenderContext CreateRenderContext(IntPtr handle)
        {
            var renderContext = new GlWindowsRenderContext(handle);

            if (m_dataContext == null)
                m_dataContext = new GlDataContext(renderContext);

            renderContext.DataContext = m_dataContext;

            return renderContext;
        }

        public override RScene CreateRScene()
        {
            return new RScene(m_dataContext);
        }

        protected override RSceneRenderer CreateRSceneRenderer()
        {
            return new DefaultGlRSceneRenderer();
        }
    }
}
