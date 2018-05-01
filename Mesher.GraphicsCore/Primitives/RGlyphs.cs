using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;

namespace Mesher.GraphicsCore.Primitives
{
    public class RGlyphs : RPrimitive
    {
        public int GlyphsTextures { get; set; }

        public int Plane { get; set; }

		public int Widths { get; set; }

		public int Heights { get; set; }

		public RGlyphs(IDataFactory dataFactory, RScene scene) : base(dataFactory, scene)
        {
        }

        public override void Render(RSceneRenderer sceneRenderer, IRenderContext renderContext)
        {
            sceneRenderer.GlyphsRenderer.Render(this, renderContext);
        }
    }
}