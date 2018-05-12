using Mesher.GraphicsCore.Collections;
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

		internal RGlyphs(IDataContext dataContext) : base(dataContext)
        {
        }

        public override void Render(RenderersFactory renderersFactory, RLights rLights, IRenderContext renderContext)
        {
            renderersFactory.GlyphsRenderer.Render(this, rLights, renderContext);
        }
    }
}