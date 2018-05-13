using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesher.GraphicsCore.Camera;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.Light;
using Mesher.GraphicsCore.Material;
using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;

namespace Mesher.GraphicsCore
{
    public abstract class MesherGraphics
    {
        //TODO encapsulate dataContext

        private RenderersFactory m_renderersFactory;

        public abstract IDataContext DataContext { get; }

        public RenderersFactory RenderersFactory
        {
            get
            {
                if (m_renderersFactory == null)
                    m_renderersFactory = CreateRSceneRenderer();
                return m_renderersFactory;
            }
        }

        public RTriangles CreateTriangles()
        {
            return new RTriangles(DataContext);
        }

        public REdges CreateEdges()
        {
            return new REdges(DataContext);
        }

        public RGlyphs CreateGlyphs()
        {
            return new RGlyphs(DataContext);
        }

        public RLight CreateLight()
        {
            return new RLight(DataContext);
        }

        public RMaterial CreateMaterial()
        {
            return new RMaterial();
        }

        public RCamera CreateCamera() 
        {
            return new RCamera();
        }

        public abstract IRenderContext CreateRenderContext(IntPtr handle);

        protected abstract RenderersFactory CreateRSceneRenderer();
    }
}
