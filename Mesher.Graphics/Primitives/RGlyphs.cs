using Mesher.Graphics.Data;
using Mesher.Graphics.Renderers;

namespace Mesher.Graphics.Primitives
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

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}