using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;

namespace Mesher.GraphicsCore.Primitives
{
    public class REdges : RPrimitive
    {
        public int Width
        {
            get => default(int);
            set
            {
            }
        }
        public REdges(IDataContext dataContext, RScene scene) : base(dataContext, scene)
        {
        }

        public override void Render(RSceneRenderer sceneRenderer, IRenderContext renderContext)
        {
            sceneRenderer.EdgesRenderer.Render(this, renderContext);
        }
    }
}