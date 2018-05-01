using Mesher.GraphicsCore.Primitives;
using Mesher.GraphicsCore.RenderContexts;

namespace Mesher.GraphicsCore.Renderers.OpenGL
{
    public class DefaultGlRSceneRenderer : RSceneRenderer
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

        public override void Render(RScene rScene, IRenderContext renderContext)
        {
            foreach (var primitive in rScene.Primitives)
                primitive.Render(this, renderContext);
        }
    }
}