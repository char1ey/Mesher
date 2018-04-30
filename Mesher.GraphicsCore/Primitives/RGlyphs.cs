using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;

namespace Mesher.GraphicsCore.Primitives
{
    public class RGlyphs : RPrimitive
    {
        public int GlyphsTextures
        {
            get => default(int);
            set
            {
            }
        }

        public int Plane
        {
            get => default(int);
            set
            {
            }
        }

        public int Widths
        {
            get => default(int);
            set
            {
            }
        }

        public int Heights
        {
            get => default(int);
            set
            {
            }
        }
        public RGlyphs(IDataContext dataContext, RScene scene) : base(dataContext, scene)
        {
        }

        public override void Render(RSceneRenderer sceneRenderer, IRenderContext renderContext)
        {
            sceneRenderer.GlyphsRenderer.Render(this, renderContext);
        }
    }
}