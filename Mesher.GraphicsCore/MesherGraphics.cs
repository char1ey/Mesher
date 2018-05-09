using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore.Data;
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

        public abstract IRenderContext CreateRenderContext(IntPtr handle);

        public abstract RScene CreateRScene();

        protected abstract RenderersFactory CreateRSceneRenderer();
    }
}
