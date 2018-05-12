using System;
using Mesher.GraphicsCore.Collections;
using Mesher.GraphicsCore.Data;
using Mesher.GraphicsCore.RenderContexts;
using Mesher.GraphicsCore.Renderers;

namespace Mesher.GraphicsCore.Primitives
{
    public class REdges : RPrimitive
    {
        public Single Width { get; set; }

        internal REdges(IDataContext dataContext) : base(dataContext)
        {
        }

        public override void Render(RenderersFactory renderersFactory, RLights rLights, IRenderContext renderContext)
        {
            renderersFactory.EdgesRenderer.Render(this, rLights, renderContext);
        }
    }
}