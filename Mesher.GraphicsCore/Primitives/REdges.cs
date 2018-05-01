using System;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;

namespace Mesher.GraphicsCore.Primitives
{
    public class REdges : RPrimitive
    {
        public Single Width { get; set; }

        public REdges(IDataFactory dataFactory, RScene scene) : base(dataFactory, scene)
        {
        }

        public override void Render(RSceneRenderer sceneRenderer, IRenderContext renderContext)
        {
            sceneRenderer.EdgesRenderer.Render(this, renderContext);
        }
    }
}