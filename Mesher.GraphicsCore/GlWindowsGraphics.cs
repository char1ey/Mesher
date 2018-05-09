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
        private GlDataFactory m_dataFactory;

        public override IDataFactory DataFactory
        {
            get { return m_dataFactory; }
        }

        public GlWindowsGraphics(GlWindowsRenderContext renderContext)
        {
            var dataContext = new GlDataFactory(renderContext);
            renderContext.DataFactory = dataContext;
            m_dataFactory = dataContext;
        }

        public override IRenderContext CreateRenderContext(IntPtr handle)
        {
            var renderContext = new GlWindowsRenderContext(handle);

            if (m_dataFactory == null)
                m_dataFactory = new GlDataFactory(renderContext);

            renderContext.DataFactory = m_dataFactory;

            return renderContext;
        }

        public override RScene CreateRScene()
        {
            return new RScene(m_dataFactory);
        }

        protected override RenderersFactory CreateRSceneRenderer()
        {
            return new DefaultGlRenderersFactory();
        }
    }
}
