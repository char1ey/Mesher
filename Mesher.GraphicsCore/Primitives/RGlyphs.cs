using Mesher.GraphicsCore.Camera;
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

        public override void Render(RenderersFactory renderersFactory, RenderArgs renderArgs)
        {
            renderersFactory.GlyphsRenderer.Render(this, renderArgs);
        }
    }
}