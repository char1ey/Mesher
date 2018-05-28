using System;
using System.Collections.Generic;
using Mesher.Graphics.Camera;
using Mesher.Graphics.Data;
using Mesher.Graphics.Light;
using Mesher.Graphics.Material;
using Mesher.Graphics.Primitives;
using Mesher.Graphics.RenderContexts;
using Mesher.Graphics.Renderers;

namespace Mesher.Graphics
{
    public abstract class MesherGraphics : IDisposable
    {
        //TODO encapsulate dataContext

        private RenderersFactory m_renderersFactory;

        private List<IDisposable> m_disposables;

        public abstract IDataContext DataContext { get; }

        public MesherGraphics()
        {
            m_disposables = new List<IDisposable>();
        }
        
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
            var rTriangles = new RTriangles(DataContext);

            m_disposables.Add(rTriangles);

            return rTriangles;
        }

        public REdges CreateEdges()
        {
            var rEdges = new REdges(DataContext);

            m_disposables.Add(rEdges);

            return rEdges;
        }

        public RGlyphs CreateGlyphs()
        {
            var rGlyphs = new RGlyphs(DataContext);

            m_disposables.Add(rGlyphs);

            return rGlyphs;
        }

        public RLight CreateLight()
        {
            var rLight = new RLight(DataContext);

            m_disposables.Add(rLight);

            return rLight;
        }

        public RMaterial CreateMaterial()
        {
            var rMaterial = new RMaterial();

            m_disposables.Add(rMaterial);

            return rMaterial;
        }

        public RCamera CreateCamera() 
        {
            var rCamera = new RCamera();

            m_disposables.Add(rCamera);

            return rCamera;
        }

        public abstract IRenderContext CreateRenderContext(IntPtr handle);

        protected abstract RenderersFactory CreateRSceneRenderer();

        public void Dispose()
        {
            foreach(var disposable in m_disposables)
                disposable.Dispose();
            RenderersFactory.Dispose();
        }
    }
}
