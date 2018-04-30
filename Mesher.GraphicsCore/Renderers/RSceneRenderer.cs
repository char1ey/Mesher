using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;

namespace Mesher.GraphicsCore.Renderers
{
    public abstract class RSceneRenderer
    {
        private RTrianglesRenderer m_trianglesRenderer;
        private REdgesRenderer m_edgesRenderer;
        private RGlyphRenderer m_glyphsRenderer;

        public RTrianglesRenderer TrianglesRenderer
        {
            get
            {
                if (m_trianglesRenderer == null)
                    m_trianglesRenderer = CreateTrianglesRenderer();
                return m_trianglesRenderer;
            }
        }
        public REdgesRenderer EdgesRenderer
        {
            get
            {
                if (m_edgesRenderer == null)
                    m_edgesRenderer = CreateEdgesRenderer();
                return m_edgesRenderer;
            }
        }

        public RGlyphRenderer GlyphsRenderer
        {
            get
            {
                if (m_glyphsRenderer == null)
                    m_glyphsRenderer = CreateGlyphsRenderer();
                return m_glyphsRenderer;
            }
        }

        protected abstract RTrianglesRenderer CreateTrianglesRenderer();
        protected abstract REdgesRenderer CreateEdgesRenderer();
        protected abstract RGlyphRenderer CreateGlyphsRenderer();
        public abstract void Render(RScene rScene, IRenderContext renderContext);
    }
}