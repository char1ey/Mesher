using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.Light;
using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;

namespace Mesher.GraphicsCore
{
    public abstract class MesherGraphics
    {
        //TODO encapsulate dataContext

        private RenderersFactory m_renderersFactory;

        public abstract IDataFactory DataFactory { get; }

        public RenderersFactory RenderersFactory
        {
            get
            {
                if (m_renderersFactory == null)
                    m_renderersFactory = CreateRSceneRenderer();
                return m_renderersFactory;
            }
        }

        public RTriangles CreateRTriangles()
        {
            return new RTriangles(DataFactory);
        }

        public REdges CreateREdges()
        {
            return new REdges(DataFactory);
        }

        public RGlyphs CreateRGlyphs()
        {
            return new RGlyphs(DataFactory);
        }

        public RLight CreateRLight()
        {
            return new RLight(DataFactory);
        }

        public abstract IRenderContext CreateRenderContext(IntPtr handle);

        protected abstract RenderersFactory CreateRSceneRenderer();
    }
}
