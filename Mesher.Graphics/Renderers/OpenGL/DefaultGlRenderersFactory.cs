namespace Mesher.Graphics.Renderers.OpenGL
{
    public class DefaultGlRenderersFactory : RenderersFactory
    {
        protected override RTrianglesRenderer CreateTrianglesRenderer()
        {
            return new DefaultGlRTrianglesRenderer();
        }

        protected override REdgesRenderer CreateEdgesRenderer()
        {
            return new DefaultGlREdgesRenderer();
        }

        protected override RGlyphRenderer CreateGlyphsRenderer()
        {
            return new DefaultGlRGlyphsRenderer();
        }

        protected override RLightRenderer CreateLightRenderer()
        {
            return new DefaultGlRLightRenderer();
        }
    }
}