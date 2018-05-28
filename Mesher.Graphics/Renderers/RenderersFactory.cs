using System;

namespace Mesher.Graphics.Renderers
{
    public abstract class RenderersFactory : IDisposable
    {
        private RTrianglesRenderer m_trianglesRenderer;
        private REdgesRenderer m_edgesRenderer;
        private RGlyphRenderer m_glyphsRenderer;
        private RLightRenderer m_lightRenderer;

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

        public RLightRenderer LightRenderer
        {
            get
            {
                if (m_lightRenderer == null)
                    m_lightRenderer = CreateLightRenderer();
                return m_lightRenderer;
            }
        }

        protected abstract RTrianglesRenderer CreateTrianglesRenderer();
        protected abstract REdgesRenderer CreateEdgesRenderer();
        protected abstract RGlyphRenderer CreateGlyphsRenderer();
        protected abstract RLightRenderer CreateLightRenderer();

        public void Dispose()
        {
        }
    }
}