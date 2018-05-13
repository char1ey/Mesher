using System;
using Mesher.Graphics.Camera;
using Mesher.Graphics.Data;
using Mesher.Graphics.Light;
using Mesher.Graphics.Material;
using Mesher.Graphics.Primitives;
using Mesher.Graphics.RenderContexts;
using Mesher.Graphics.Renderers;

namespace Mesher.Graphics
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
