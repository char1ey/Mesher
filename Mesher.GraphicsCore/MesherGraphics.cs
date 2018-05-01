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

        private RSceneRenderer m_rSceneRenderer;

        public abstract IDataFactory DataFactory { get; }

        public abstract IRenderContext CreateRenderContext(IntPtr handle);

        public abstract RScene CreateRScene();

        public RSceneRenderer GetRSceneRenderer()
        {
            if (m_rSceneRenderer == null)
                m_rSceneRenderer = CreateRSceneRenderer();
            return m_rSceneRenderer;
        }

        protected abstract RSceneRenderer CreateRSceneRenderer();
    }
}
